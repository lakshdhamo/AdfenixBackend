using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.Services.Interface;
using MediatR;

namespace Adfenix.Handlers.CommandHandlers
{
    /// <summary>
    /// Send data handler
    /// </summary>
    public class SendDataCommandHandler : IRequestHandler<SendDataRequestDto>
    {
        private readonly IDataWriteService _dataWriteService;
        private readonly ILogService _logService;
        public SendDataCommandHandler(ILogService logService, IDataWriteService dataWriteService)
        {
            _dataWriteService = dataWriteService;
            _logService = logService;
        }

        /// <summary>
        /// Handles send data command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SendDataRequestDto request, CancellationToken cancellationToken)
        {
            _logService.LogInfo("SendDataCommandHandler called.");
            await _dataWriteService.SendDataAsync(request);
            return Unit.Value;
        }


    }
}
