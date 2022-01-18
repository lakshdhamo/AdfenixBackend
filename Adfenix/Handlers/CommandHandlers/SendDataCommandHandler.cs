using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.Services.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Handlers.CommandHandlers
{
    public class SendDataCommandHandler : IRequestHandler<SendDataRequestDto>
    {
        private readonly IDataWriteService _dataWriteService;
        private readonly ILogService _logService;
        public SendDataCommandHandler(ILogService logService, IDataWriteService dataWriteService)
        {
            _dataWriteService = dataWriteService;
            _logService = logService;
        }

        public async Task<Unit> Handle(SendDataRequestDto request, CancellationToken cancellationToken)
        {

            _logService.LogInfo("SendDataCommandHandler called.");
            await _dataWriteService.SendDataAsync(request);
            return Unit.Value;
        }


    }
}
