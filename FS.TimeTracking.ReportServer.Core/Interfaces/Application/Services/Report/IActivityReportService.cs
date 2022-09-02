using FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Report;

/// <summary>
/// Time sheet report service.
/// </summary>
public interface IActivityReportService
{
    /// <summary>
    /// Generates an activity report.
    /// </summary>
    /// <param name="source">Source for the report.</param>
    /// <param name="cancellationToken">a token that allows processing to be cancelled.</param>
    Task<FileResult> GenerateActivityReport(ActivityReportDto source, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates preview pages for detailed activity report.
    /// </summary>
    /// <param name="source">Source for the report.</param>
    /// <param name="pageFrom">The first page to get the preview for.</param>
    /// <param name="pageTo">The last page to get the preview for.</param>
    /// <param name="cancellationToken">a token that allows processing to be cancelled.</param>
    Task<ReportPreviewDto> GenerateActivityReportPreview(ActivityReportDto source, int pageFrom, int pageTo, CancellationToken cancellationToken = default);
}