using Adfenix.Helper;
using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Adfenix
{
    internal partial class Program
    {
        private readonly ILogService _logService;
        private readonly IMediator _mediator;

        public Program(ILogService logService, IMediator mediator)
        {
            _logService = logService;
            _mediator = mediator;
        }

        /// <summary>
        /// Main method to configure and run application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            HttpClientManager.HttpClientFactory = host.Services.GetService<IHttpClientFactory>();
            ConfigureConstantValues(host);
            host.Services.GetRequiredService<Program>().Run();
        }

        /// <summary>
        /// Runs the application. Trigger point
        /// </summary>
        public void Run()
        {
            _logService.LogInfo("Program started.");

            var epochTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            short[] serverIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };


            _ = Parallel.ForEach(serverIds, serverId =>
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                };

                string value = FetchServerCount(serverId);
                _logService.LogInfo($"Fetched count from Server: {serverId}");

                SendData($"Campaign.{serverId}", value, epochTimestamp);
                _logService.LogInfo($"SendData completed for Server: {serverId}");
            });

            string value = ZendeskQueueCount();
            _logService.LogInfo("Fetched count from ZendeskQueue");

            SendData("Zendesk.Metric", value, epochTimestamp);
            _logService.LogInfo("Program completed.");
        }

        /// <summary>
        /// Sends data to server
        /// </summary>
        /// <param name="value"></param>
        /// <param name="metric"></param>
        /// <param name="epochTimestamp"></param>
        private void SendData(string value, string metric, int epochTimestamp)
        {
            SendDataRequestDto requestModel = new()
            {
                epochTimestamp = epochTimestamp,
                Metric = metric,
                Value = value,
                VisualiserSeriesUri = ConstantManager.VisualiserSeriesUri,
                VisualiserApiKey = ConstantManager.VisualiserApiKey
            };
            _mediator.Send(requestModel);
        }

        /// <summary>
        /// Gets cound from server
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string FetchServerCount(int number)
        {
            ServerCountRequestDto requestServer = new()
            {
                ServerId = number
            };
            return _mediator.Send(requestServer).Result;
        }

        /// <summary>
        /// Gets count from ZendeskQueue
        /// </summary>
        /// <returns></returns>
        private string ZendeskQueueCount()
        {
            ZendeskQueueCountRequestDto requestZendesk = new()
            {
                Url = ConstantManager.CaseManagementQueueCountUrl,
                Token = ConstantManager.CaseManagementAuthToken
            };
            return _mediator.Send(requestZendesk).Result;
        }

    }
}