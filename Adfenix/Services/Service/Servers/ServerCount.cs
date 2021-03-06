using Adfenix.Helper;
using System.Text.RegularExpressions;

namespace Adfenix.Services.Service.Servers
{
    public class ServerCount : ServerBase
    {
        int serverId;

        public ServerCount()
        {
            /// Setting default Parameters
            parameters = new Parameter[1];
            parameters[0] = new IntParameter("Server Id", 1, 100, 1);
        }

        /// <summary>
        /// Gets count from server
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
        /// Handles server count fetch logic
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetServerCount()
        {
            /// Gets parameter value
            serverId = ((IntParameter)parameters[0]).GetValue();

            ///Fetch logic
            string result = string.Empty;
            var url = $"http://{serverId}.localhost.com/count";
            var _httpClient = HttpClientManager.HttpClientFactory.CreateClient();
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using HttpContent content = response.Content;
                var htmlCode = await content.ReadAsStringAsync();

                const string newCount = "new count: (.*)";
                var match = new Regex(newCount, RegexOptions.IgnoreCase).Match(htmlCode);
                var campaignCount = match.Groups[1].Value;
                result = campaignCount;
            }
            return result;

        }
    }
}
