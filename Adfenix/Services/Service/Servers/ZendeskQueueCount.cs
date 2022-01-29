using Adfenix.Helper;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Adfenix.Services.Service.Servers
{
    public class ZendeskQueueCount : ServerBase
    {
        string url;
        string token;

        public ZendeskQueueCount()
        {
            /// Setting default Parameters
            parameters = new Parameter[2];
            parameters[0] = new StrParameter("Url", "");
            parameters[1] = new StrParameter("Token", "");
        }

        /// <summary>
        /// Gets count from ZendeskQueue
        /// </summary>
        /// <returns></returns>
        public override async Task<string> FetchCountAsync()
        {
            string result = "";

            /// Perform action with retry
            await RetryHelper.RetryOnExceptionAsync<Exception>
                          (3, TimeSpan.FromSeconds(2), async () =>
                          {
                              result = await GetServerCount();
                          });

            return result;
        }

        /// <summary>
        /// Handles ZendeskQueue count fetch logic
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetServerCount()
        {
            /// Gets parameter value
            url = ((StrParameter)parameters[0]).GetValue();
            token = ((StrParameter)parameters[1]).GetValue();

            ///Fetch logic
            string result = string.Empty;
            var _httpClient = HttpClientManager.HttpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using HttpContent content = response.Content;
                var json = await content.ReadAsStringAsync();

                var queueCount = JObject.Parse(json)["count"]?.ToString();
                Console.WriteLine($"Zendesk Engineering Ticket count: {queueCount}");
                return queueCount ?? string.Empty;
            }
            return result;
        }

    }
}
