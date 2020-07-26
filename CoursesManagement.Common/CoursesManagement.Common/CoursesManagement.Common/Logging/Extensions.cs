using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
            => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var appOptions = context.Configuration.GetOptions<AppOptions>("app");
                var seqOptions = context.Configuration.GetOptions<SeqOptions>("seq");
                var serilogOptions = context.Configuration.GetOptions<SerilogOptions>("serilog");
                if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
                {
                    level = LogEventLevel.Information;
                }

                applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;
                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);
                Configure(loggerConfiguration, level, seqOptions, serilogOptions);
            });

        private static void Configure(LoggerConfiguration loggerConfiguration, LogEventLevel level,
            SeqOptions seqOptions, SerilogOptions serilogOptions)
        {
            if (seqOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
            }

            if (serilogOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console();
            }
        }
    }
}
