using FS.TimeTracking.ReportServer.Application.Services.Report;
using FS.TimeTracking.ReportServer.Application.Services.Shared;
using FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Report;
using FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace FS.TimeTracking.ReportServer.Startup;

internal static class DependencyConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IInformationService, InformationService>();
        services.AddScoped<IActivityReportService, ActivityReportService>();
        return services;
    }
}