using Adfenix.Helper;
using Adfenix.Services.Interface;
using Adfenix.Services.Service;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Adfenix
{
    internal partial class Program
    {
        /// <summary>
        /// Configure hosting settings
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostEnvironment env = context.HostingEnvironment;
                    config.AddEnvironmentVariables()
                        // copy configuration files to output directory
                        .AddJsonFile("appsettings.json")
                        // default prefix for environment variables is DOTNET_
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddCommandLine(args);
                })
                .ConfigureServices(services =>
                {
                    services.AddTransient<Program>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());
                    services.AddScoped<IDataReadService, DataReadService>();
                    services.AddScoped<IDataWriteService, DataWriteService>();
                    services.AddScoped<ILogService, LogService>();
                    services.AddLogging(configure => configure.AddConsole())
                        .AddTransient<LogService>();
                });
        }

        /// <summary>
        /// Configure Constant values
        /// </summary>
        /// <param name="host"></param>
        private static void ConfigureConstantValues(IHost host)
        {
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            ConstantManager.VisualiserSeriesUri = config["VisualiserSeriesUri"];
            ConstantManager.VisualiserApiKey = config["VisualiserApiKey"];
            ConstantManager.CaseManagementQueueCountUrl = config["CaseManagementQueueCountUrl"];
            ConstantManager.CaseManagementAuthToken = config["CaseManagementAuthToken"];

        }
    }
}
