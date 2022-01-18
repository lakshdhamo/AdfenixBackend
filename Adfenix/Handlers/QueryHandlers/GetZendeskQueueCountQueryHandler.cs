using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Handlers.QueryHandlers
{
    /// <summary>
    /// Get ZendeskQueue count handler
    /// </summary>
    public class GetZendeskQueueCountQueryHandler : IRequestHandler<ZendeskQueueCountRequestDto, string>
    {
        private readonly IDataReadService _dataReadService;
        private readonly ILogService _logService;

        public GetZendeskQueueCountQueryHandler(ILogService logService, IDataReadService dataReadService)
        {
            _dataReadService = dataReadService;
            _logService = logService;
        }

        /// <summary>
        /// Handles ZendeskQueue count handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(ZendeskQueueCountRequestDto request, CancellationToken cancellationToken)
        {
            _logService.LogInfo("GetZendeskQueueCountQueryHandler called.");
            return await _dataReadService.GetServerCountById(request);
        }
    }
}
