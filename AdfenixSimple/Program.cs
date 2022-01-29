using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace AdfenixSimple
{
    internal partial class Program
    {
        private static string VisualiserSeriesUri;
        private static string VisualiserApiKey;
        private static string CaseManagementQueueCountUrl;
        private static string CaseManagementAuthToken;
        private static IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Main method to configure and run application
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            /// To Avoid Socket Exhaustion
            httpClientFactory = host.Services.GetService<IHttpClientFactory>();
            ConfigureConstantValues(host);
            await Run();
        }

        public async static Task Run()
        {
            Console.WriteLine("Program started.");
            short[] serverIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            _ = Parallel.ForEach(serverIds, async serverId =>
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                };

                // Fetch value from Server and send
                await FetchServerCountAsync(serverId).ContinueWith(async (value) =>
                await SendDataAsync($"Campaign.{serverId}", value.Result)
                );

            });

            // Fetch value from ZendeskQueue and send
            await ZendeskQueueCountAsync().ContinueWith(async (value) =>
            await SendDataAsync("Zendesk.Metric", value.Result)
            );

            Console.WriteLine("Program completed.");
        }

        /// <summary>
        /// Sends data to server
        /// </summary>
        /// <param name="value"></param>
        /// <param name="metric"></param>
        /// <param name="epochTimestamp"></param>
        private static async Task SendDataAsync(string metric, string value)
        {
            try
            {
                var epochTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                var json = "{'series':[{'metric':'" + metric + "','points':[[" + epochTimestamp + "," + value + "]],'type':'count'}]}";
                json = json.Replace("'", "\"");

                var _httpClient = httpClientFactory.CreateClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(VisualiserSeriesUri + "?api_key=" + VisualiserApiKey, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    Console.WriteLine($"SendData is successful with {value} - {metric}", value, metric);
                }
                else
                {
                    Console.WriteLine($"SendData is failed with {value} - {metric}", value, metric);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets cound from server
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Server count. Empty string will be returned in case of Exception</returns>
        private static async Task<string> FetchServerCountAsync(int serverId)
        {
            string result = string.Empty;
            try
            {
                ///Fetch logic
                var url = $"http://{serverId}.localhost.com/count";
                var _httpClient = httpClientFactory.CreateClient();
                using HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using HttpContent content = response.Content;
                    var htmlCode = await content.ReadAsStringAsync();

                    const string newCount = "new count: (.*)";
                    var match = new Regex(newCount, RegexOptions.IgnoreCase).Match(htmlCode);
                    var campaignCount = match.Groups[1].Value;
                    Console.WriteLine($"Server: {serverId}   Campaign Queue Size: {campaignCount}");
                    result = campaignCount;
                }
                else
                {
                    Console.WriteLine($"FetchCount failed for Server: {serverId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets count from ZendeskQueue
        /// </summary>
        /// <returns>Server count. Empty string will be returned in case of Exception</returns>
        private static async Task<string> ZendeskQueueCountAsync()
        {
            string result = string.Empty;
            try
            {
                ///Fetch logic
                var _httpClient = httpClientFactory.CreateClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", CaseManagementAuthToken);
                using HttpResponseMessage response = await _httpClient.GetAsync(CaseManagementQueueCountUrl);
                if (response.IsSuccessStatusCode)
                {
                    using HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();

                    var queueCount = JObject.Parse(json)["count"]?.ToString();
                    Console.WriteLine($"Zendesk Engineering Ticket count: {queueCount}");
                    result = queueCount ?? String.Empty;
                }
                else
                {
                    Console.WriteLine($"ZendeskQueue fetchcount is failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return String.Empty;
        }

    }
}