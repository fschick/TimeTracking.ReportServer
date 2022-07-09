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
    private readonly TimeTrackingReportConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityReportService"/> class.
    /// </summary>
    public ActivityReportService(IOptions<TimeTrackingReportConfiguration> configuration)
        => _configuration = configuration.Value;

    /// <inheritdoc />
    public Task<FileResult> GenerateActivityReport(ActivityReportDto reportDto, CancellationToken cancellationToken = default)
    {
        using var report = GetActivityReportInternal(reportDto);
        var reportPdf = StiNetCoreReportResponse.ResponseAsPdf(report);

        var reportFileResult = new FileContentResult(reportPdf.Data, "application/pdf")
        {
            FileDownloadName = reportPdf.FileName
        };

        return Task.FromResult((FileResult)reportFileResult);
    }

    /// <inheritdoc />
    public async Task<ReportPreviewDto> GenerateActivityReportPreview(ActivityReportDto reportDto, int pageFrom, int pageTo, CancellationToken cancellationToken = default)
    {
        using var report = GetActivityReportInternal(reportDto);
        await report.RenderAsync();

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
        }

        return result;
    }

    private StiReport GetActivityReportInternal(ActivityReportDto reportDto)
    {
        StiLicense.Key = _configuration.StimulsoftLicenseKey;
        var report = StiReport.CreateNewReport();
        var reportFolder = Path.Combine(TimeTrackingReportConfiguration.ExecutablePath, TimeTrackingReportConfiguration.REPORT_FOLDER);
        var reportFile = Path.Combine(reportFolder, "ActivityReport.Detailed.mrt");
        report.Load(reportFile);
        report.Dictionary.Databases.Clear();

        var reportDataJson = JsonConvert.SerializeObject(reportDto);
        using var reportData = StiJsonToDataSetConverterV2.GetDataSet(reportDataJson);
        report.RegData("TimeSheet", reportData);

        var customers = reportDto.TimeSheets.Select(x => x.CustomerTitle).Distinct().OrderBy(x => x);
        report.ReportName = $"{reportDto.Translations["Title"]} - {reportDto.Parameters.StartDate:yyyy-MM-dd} - {reportDto.Parameters.EndDate:yyyy-MM-dd} - {string.Join(", ", customers)}";

        return report;
    }
}