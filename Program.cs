using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp_Investigate.AppExtensions;

namespace WebApp_Investigate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(logging =>
                //{
                //    //Remove all the ILoggerProvider instances from the builder
                //    logging.ClearProviders();
                //    //Adds the Console logging provider
                //    logging.AddConsole();
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                ////Logging to file
                //.ConfigureLogging((hostBuilderContext, logging) =>
                //{
                //    logging.AddRoundTheCodeFileLogger(options =>
                //    {
                //        hostBuilderContext.Configuration.GetSection("Logging").GetSection("RoundTheCodeFile").GetSection("Options").Bind(options);
                //    });
                //})
                
            ;
    }
}
