using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using MediatR;

namespace Adfenix.QueryHandlers
{
    /// <summary>
    /// Get Server count handler
    /// </summary>
    public class GetServerCountByIdQueryHandler : IRequestHandler<ServerCountRequestDto, string>
    {
        private readonly IDataReadService _dataReadService;
        private readonly ILogService _logService;
        public GetServerCountByIdQueryHandler(ILogService logService, IDataReadService dataReadService)
        {
            _dataReadService = dataReadService;
            _logService = logService;
        }

        /// <summary>
        /// Handles Server data count handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(ServerCountRequestDto request, CancellationToken cancellationToken)
        {
            _logService.LogInfo("GetServerCountByIdQueryHandler called.");
            return await _dataReadService.GetServerCountById(request.ServerId);
        }
    }
}
