using System.Collections.Generic;

namespace FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;

/// <summary>
/// A report preview data transfer object.
/// </summary>
public class ReportPreviewDto
{
    /// <summary>
    /// Gets or sets the total page count.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the preview pages as PNG.
    /// </summary>
    public List<byte[]> Pages { get; set; }
}