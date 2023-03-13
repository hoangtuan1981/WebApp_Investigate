**Link**
https://www.roundthecode.com/dotnet/how-aspnet-core-logging-works-ilogger-loglevel?utm_source=youtube&utm_medium=referral&utm_campaign=NN9Rmf0PUG4+-+video+content+%28more+information%29

From microsoft:
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1#configure-logging

**Cases**
1. Basic logging, Disabel logging to console
2. logging to file
3. log4net and Serilog
4. The Microsoft.Extensions.Logging.AzureAppServices: log to azure blob text file

ILogger interface can be used when building an ASP.NET Core. With the ILogger interface, we can use a logging provider which logs to a particular format. There are a number of logging providers available including log4net and Serilog.
Microsoft.Extensions.Logging NuGet package provides us with a number of extension methods that allows us to log at different levels.
In-addition, we can also build a custom logging provider that allows us to build a .NET logging library and allows us to format a log in the way we want it written.

**Descriptions**
Inside the Logging object, there is a LogLevel object in appsettings.json file When create netcore project.
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
      //,"WebApp_Investigate.Controllers": "None" --> disable logging in controllers
    }
  },
  "AllowedHosts": "*"
}
Logging --> LogLevel: set for all providers.

**Disable logging**
"WebApp_Investigate.Controllers": "None" --> disable logging in controllers namespace.

**log4net**
Link: https://github.com/scottluskcis/log4net-netcore-example
The output of the log file is placed in Log4NetInCoreExampleLogFile.log in the bin folder when you run the application

**serilog**
