﻿using Adfenix.RequestModels.CommandRequestModels;
using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using Adfenix.Services.Service.Servers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adfenix.Services.Service
{
    /// <summary>
    /// Handles all the read operations
    /// </summary>
    public class DataReadService : IDataReadService
    {
        private readonly ILogService _logService;
        public DataReadService(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Gets count from Server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public async Task<string> GetServerCountById(int serverId)
        {
            try
            {
                Server server = new ServerCount();
                Parameter[] parameters = server.getParameters();
                ((IntParameter)parameters[0]).SetValue(serverId);

                return await server.ExecuteAsync();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets count from ZendeskQueue
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> GetServerCountById(ZendeskQueueCountRequestDto input)
        {
            try
            {
                Server server = new ZendeskQueueCount();
                Parameter[] parameters = server.getParameters();
                ((StrParameter)parameters[0]).SetValue(input.Url);
                ((StrParameter)parameters[0]).SetValue(input.Token);

                return await server.ExecuteAsync();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return String.Empty;
            }
        }



    }
}
