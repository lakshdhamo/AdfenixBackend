using Adfenix.Helper;
using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.Services.Interface;
using System.Text;

namespace Adfenix.Services.Service
{
    /// <summary>
    /// Handles all the write operations
    /// </summary>
    public class DataWriteService : IDataWriteService
    {
        private readonly ILogService _logService;
        public DataWriteService(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Send the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendDataAsync(SendDataRequestDto data)
        {
            try
            {
                /// Perform action with retry
                await RetryHelper.RetryOnExceptionAsync<Exception>
                              (3, TimeSpan.FromSeconds(2), async () =>
                              {
                                  await PostDataAsync(data);
                              });
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// Send data logic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task PostDataAsync(SendDataRequestDto data)
        {
            var json = "{'series':[{'metric':'" + data.Metric + "','points':[[" + data.epochTimestamp + "," + data.Value + "]],'type':'count'}]}";
            json = json.Replace("'", "\"");

            using HttpClient _httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(data.VisualiserSeriesUri + "?api_key=" + data.VisualiserApiKey, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }


    }
}
