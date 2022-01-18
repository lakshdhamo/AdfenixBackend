using Adfenix.Helper;
using System.Text.RegularExpressions;

namespace Adfenix.Services.Service.Servers
{
    public class ServerCount : Server
    {
        int serverId;

        public ServerCount()
        {
            parameters = new Parameter[1];
            parameters[0] = new IntParameter("Server Id", 1, 100, 1);

            serverId = ((IntParameter)parameters[0]).GetValue();
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
            var url = $"http://{serverId}.localhost.com/count";

            using HttpClient _httpClient = new HttpClient();
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            using HttpContent content = response.Content;
            var htmlCode = await content.ReadAsStringAsync();

            const string newCount = "new count: (.*)";
            var match = new Regex(newCount, RegexOptions.IgnoreCase).Match(htmlCode);
            var campaignCount = match.Groups[1].Value;
            Console.WriteLine($"Server: {serverId}   Campaign Queue Size: {campaignCount}");
            return campaignCount;

        }
    }
}
