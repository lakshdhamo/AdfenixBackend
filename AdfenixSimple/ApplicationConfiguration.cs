using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfenixSimple
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
                });
        }

        /// <summary>
        /// Configure Constant values
        /// </summary>
        /// <param name="host"></param>
        private static void ConfigureConstantValues(IHost host)
        {
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            VisualiserSeriesUri = config["VisualiserSeriesUri"];
            VisualiserApiKey = config["VisualiserApiKey"];
            CaseManagementQueueCountUrl = config["CaseManagementQueueCountUrl"];
            CaseManagementAuthToken = config["CaseManagementAuthToken"];

        }
    }
}
