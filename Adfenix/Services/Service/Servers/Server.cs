namespace Adfenix.Services.Service.Servers
{
    /// <summary>
    /// This is Server context class. 
    /// It maintains a reference to a Server object
    /// </summary>
    public class Server
    {
        private readonly ServerBase _serverBase;
        public Server(ServerBase serverBase)
        {
            _serverBase = serverBase;
        }

        /// <summary>
        /// Gets count from plugged Server
        /// </summary>
        /// <returns></returns>
        public async Task<string> FetchCountAsync()
        {
            return await _serverBase.FetchCountAsync();
        }
    }
}
