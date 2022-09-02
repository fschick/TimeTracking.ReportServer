using FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;
using FS.TimeTracking.ReportServer.Api.REST.Routing;
using FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Report;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FS.TimeTracking.ReportServer.Api.REST.Controllers.Report;

/// <summary>
/// A controller for handling time sheet reports.
/// </summary>
[V1ApiController]
public class ActivityReportController : ControllerBase, IActivityReportService
{
    private readonly IActivityReportService _activityReportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityReportController"/> class.
    /// </summary>
    /// <param name="activityReportService">The time sheet report service.</param>
    public ActivityReportController(IActivityReportService activityReportService)
        => _activityReportService = activityReportService;

    /// <inheritdoc cref="IActivityReportService.GenerateActivityReport" />
    [HttpPost]
    [ProducesResponseType(typeof(byte[]), 200)]
    public async Task<FileResult> GenerateActivityReport(ActivityReportDto source, CancellationToken cancellationToken = default)
        => await _activityReportService.GenerateActivityReport(source, cancellationToken);

    /// <inheritdoc cref="IActivityReportService.GenerateActivityReportPreview" />
    [HttpPost]
    public async Task<ReportPreviewDto> GenerateActivityReportPreview(ActivityReportDto source, int pageFrom, int pageTo, CancellationToken cancellationToken = default)
        => await _activityReportService.GenerateActivityReportPreview(source, pageFrom, pageTo, cancellationToken);
}