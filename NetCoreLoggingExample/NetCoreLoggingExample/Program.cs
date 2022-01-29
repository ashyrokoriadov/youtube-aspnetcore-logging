using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace NetCoreLoggingExample
{
    public class Program
    {  
        public static void Main(string[] args)
        {  
            //StartApplicationSimply(args);
            StartApplicationWithNlog(args);
        }

        private static void StartApplicationSimply(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static void StartApplicationWithNlog(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug("Start application");

                CreateHostBuilderWithNLog(args) .Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            return builder;
        }

        private static IHostBuilder CreateHostBuilderWithNLog(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();

            return builder;
        }
    }
}
