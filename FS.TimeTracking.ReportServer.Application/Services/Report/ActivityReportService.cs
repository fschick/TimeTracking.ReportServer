using FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;
using FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Report;
using FS.TimeTracking.ReportServer.Core.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FS.TimeTracking.ReportServer.Application.Services.Report;

/// <inheritdoc />
public class ActivityReportService : IActivityReportService
{
    private const string DETAILED_ACTIVITY_REPORT_FILE = "ActivityReport.Detailed.mrt";

    private readonly TimeTrackingReportConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityReportService"/> class.
    /// </summary>
    public ActivityReportService(IOptions<TimeTrackingReportConfiguration> configuration)
        => _configuration = configuration.Value;

    /// <inheritdoc />
    public Task<FileResult> GenerateDetailedActivityReport(ActivityReportDto reportDto, CancellationToken cancellationToken = default)
        => GenerateReport(reportDto, DETAILED_ACTIVITY_REPORT_FILE, cancellationToken);

    /// <inheritdoc />
    public async Task<ReportPreviewDto> GenerateDetailedActivityReportPreview(ActivityReportDto reportDto, int pageFrom, int pageTo, CancellationToken cancellationToken = default)
        => await GenerateReportPreview(reportDto, DETAILED_ACTIVITY_REPORT_FILE, pageFrom, pageTo, cancellationToken);

    private async Task<FileResult> GenerateReport(ActivityReportDto reportDto, string reportFile, CancellationToken cancellationToken)
    {
        using var report = CreateActivityReportInternal(reportDto, reportFile);
        report.StatusChanged += (_, _) => report.IsStopped = cancellationToken.IsCancellationRequested;
        await report.RenderAsync();

        if (cancellationToken.IsCancellationRequested)
            return null;

        var reportPdf = StiNetCoreReportResponse.ResponseAsPdf(report);
        var reportFileResult = new FileContentResult(reportPdf.Data, "application/pdf")
        {
            FileDownloadName = reportPdf.FileName
        };

        return reportFileResult;
    }

    private async Task<ReportPreviewDto> GenerateReportPreview(ActivityReportDto reportDto, string reportFile, int pageFrom, int pageTo, CancellationToken cancellationToken)
    {
        using var report = CreateActivityReportInternal(reportDto, reportFile);
        report.StatusChanged += (_, _) => report.IsStopped = cancellationToken.IsCancellationRequested;
        await report.RenderAsync();

        if (cancellationToken.IsCancellationRequested)
            return null;

        var result = new ReportPreviewDto
        {
            TotalPages = report.GetTotalRenderedPageCount(),
            Pages = new List<byte[]>()
        };

        //return StiNetCoreReportResponse.ResponseAsSvg(report, new StiImageExportSettings { ImageFormat = StiImageFormat.Color, ImageType = StiImageType.Svg, PageRange = new StiPagesRange("1-4") });
        for (var currentPage = Math.Max(pageFrom, 1); currentPage <= pageTo && currentPage <= result.TotalPages; currentPage++)
        {
            var exportSettings = new StiImageExportSettings(StiImageType.Png) { MultipleFiles = false, PageRange = new StiPagesRange($"{currentPage}-{currentPage}") };
            var page = StiNetCoreReportResponse.ResponseAsPng(report, exportSettings, false);
            result.Pages.Add(page.Data);

            if (cancellationToken.IsCancellationRequested)
                return null;
        }

        return result;
    }

    private StiReport CreateActivityReportInternal(ActivityReportDto reportDto, string fileName)
    {
        StiLicense.Key = _configuration.StimulsoftLicenseKey;
        var report = StiReport.CreateNewReport();
        var reportFolder = Path.Combine(TimeTrackingReportConfiguration.ExecutablePath, TimeTrackingReportConfiguration.REPORT_FOLDER);
        var reportFile = Path.Combine(reportFolder, fileName);
        report.Load(reportFile);
        report.Dictionary.Databases.Clear();

        report.Culture = reportDto.Parameters.Culture;

        var reportDataJson = JsonConvert.SerializeObject(reportDto);
        using var reportData = StiJsonToDataSetConverterV2.GetDataSet(reportDataJson);
        report.RegData("TimeSheet", reportData);

        var customers = reportDto.TimeSheets.Select(x => x.CustomerTitle).Distinct().OrderBy(x => x);
        report.ReportName = $"{reportDto.Translations["Title"]} - {reportDto.Parameters.StartDate:yyyy-MM-dd} - {reportDto.Parameters.EndDate:yyyy-MM-dd} - {string.Join(", ", customers)}";

        return report;
    }
}