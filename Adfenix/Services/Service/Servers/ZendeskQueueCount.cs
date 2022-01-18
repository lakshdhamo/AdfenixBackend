using Adfenix.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Service.Servers
{
    public class ZendeskQueueCount : Server
    {
        string url;
        string token;

        public ZendeskQueueCount()
        {
            parameters = new Parameter[2];
            parameters[0] = new StrParameter("Url", "");
            parameters[1] = new StrParameter("Token", "");

            url = ((StrParameter)parameters[0]).GetValue();
            token = ((StrParameter)parameters[1]).GetValue();
        }

        public override async Task<string> ExecuteAsync()
        {
            string result = "";

            await RetryHelper.RetryOnExceptionAsync<Exception>
                          (3, TimeSpan.FromSeconds(2), async () =>
                          {
                              result = await GetServerCount();
                          });

            return result;
        }

        private async Task<string> GetServerCount()
        {
            using HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            using HttpContent content = response.Content;
            var json = await content.ReadAsStringAsync();

            var queueCount = JObject.Parse(json)["count"]?.ToString();
            Console.WriteLine($"Zendesk Engineering Ticket count: {queueCount}");
            return queueCount;
        }

    }
}
