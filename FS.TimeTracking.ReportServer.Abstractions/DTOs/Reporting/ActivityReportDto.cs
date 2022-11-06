using System.Collections.Generic;

namespace FS.TimeTracking.ReportServer.Abstractions.DTOs.Reporting;

/// <summary>
/// Time sheet report data.
/// </summary>
public class ActivityReportDto
{
    /// <inheritdoc cref="ReportParameterDto" />
    public ReportParameterDto Parameters { get; set; }

    /// <inheritdoc cref="ServiceProviderDto" />
    public ServiceProviderDto ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the translations.
    /// </summary>
    public Dictionary<string, string> Translations { get; set; }

    /// <summary>
    /// Gets or sets the time sheets.
    /// </summary>
    public List<ActivityReportTimeSheetDto> TimeSheets { get; set; }
}