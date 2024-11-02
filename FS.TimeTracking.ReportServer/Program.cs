using FS.TimeTracking.ReportServer.Api.REST.Startup;
using FS.TimeTracking.ReportServer.Core.Models.Configuration;
using FS.TimeTracking.ReportServer.Startup;
using Microsoft.AspNetCore.Builder;
using NLog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace FS.TimeTracking.ReportServer;

internal class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var options = new CommandLineOptions(args);
            if (!options.Parsed)
                return;

            await using var webApp = TimeTrackingWebApp.Create(args);

            if (options.GenerateOpenApiSpecFile)
                webApp.GenerateOpenApiSpec(options.OpenApiSpecFile);
            else
                await webApp
                    .RunAsync();
        }
        catch (Exception exception)
        {
            var loggerConfiguration = Path.Combine(TimeTrackingReportConfiguration.CONFIG_FOLDER, TimeTrackingReportConfiguration.NLOG_CONFIGURATION_FILE);
            var logger = LogManager
                .Setup()
                .LoadConfigurationFromFile(loggerConfiguration)
                .LogFactory
                .GetCurrentClassLogger();

            logger.Error(exception, "Program stopped due to an exception");

            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Required by EF")]
    public static WebApplicationBuilder CreateHostBuilder(string[] args)
        => TimeTrackingWebApp.CreateWebApplicationBuilder(args);
}