using Adfenix.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Service
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation("Info: " + message + " - {date}", DateTime.Now);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning("Waring: " + message + " - {date}", DateTime.Now);
        }

        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, message + " - {date}", DateTime.Now);
        }

    }
}
