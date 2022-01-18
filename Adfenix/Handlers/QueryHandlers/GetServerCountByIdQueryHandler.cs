﻿using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.QueryHandlers
{
    public class GetServerCountByIdQueryHandler : IRequestHandler<ServerCountRequestDto, string>
    {
        private readonly IDataReadService _dataReadService;
        private readonly ILogService _logService;
        public GetServerCountByIdQueryHandler(ILogService logService, IDataReadService dataReadService)
        {
            _dataReadService = dataReadService;
            _logService = logService;
        }

        public async Task<string> Handle(ServerCountRequestDto request, CancellationToken cancellationToken)
        {
            _logService.LogInfo("GetServerCountByIdQueryHandler called.");
            return await _dataReadService.GetServerCountById(request.ServerId);
        }
    }
}