using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WebApp_Investigate.Models;
using WebApp_Investigate.Providers;

namespace WebApp_Investigate.AppExtensions
{
    public static class RoundTheCodeFileLoggerExtensions
    {
        public static ILoggingBuilder AddRoundTheCodeFileLogger(this ILoggingBuilder builder, Action<RoundTheCodeFileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, RoundTheCodeFileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
