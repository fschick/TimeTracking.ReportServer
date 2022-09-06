using FS.TimeTracking.ReportServer.Abstractions.Attributes.Validation;

namespace FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;

/// <summary>
/// Provider information.
/// </summary>
public class ProviderDto
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the company.
    /// </summary>
    public string Company { get; set; }

    /// <summary>
    /// Gets or sets the department.
    /// </summary>
    public string Department { get; set; }

    /// <summary>
    /// Gets or sets the provider logo.
    /// </summary>
    [Image(MaxFileSize = 2 * 1024 * 1024 /*2 MB */, MaxImageWidth = 2000, MaxImageHeight = 2000)]
    public byte[] Logo { get; set; }
}