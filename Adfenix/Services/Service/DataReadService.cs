using Adfenix.RequestModels.QueryRequestModels;
using Adfenix.Services.Interface;
using Adfenix.Services.Service.Servers;

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
                /// Prepare Server - Generic server based on Server Id
                ServerBase server = new ServerCount();
                Parameter[] parameters = server.getParameters();
                ((IntParameter)parameters[0]).SetValue(serverId);

                /// Call Server
                return await new Server(server).FetchCountAsync();
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
                /// Prepare Server - ZendeskQueue
                ServerBase server = new ZendeskQueueCount();
                Parameter[] parameters = server.getParameters();
                ((StrParameter)parameters[0]).SetValue(input.Url);
                ((StrParameter)parameters[0]).SetValue(input.Token);

                /// Call Server
                return await new Server(server).FetchCountAsync();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return String.Empty;
            }
        }



    }
}
