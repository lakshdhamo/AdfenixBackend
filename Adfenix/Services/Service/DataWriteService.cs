using Adfenix.Helper;
using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Service
{
    public class DataWriteService : IDataWriteService
    {
        private readonly ILogService _logService;
        public DataWriteService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task SendDataAsync(SendDataRequestDto data)
        {
            try
            {
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
