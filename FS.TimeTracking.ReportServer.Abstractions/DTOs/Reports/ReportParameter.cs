﻿using System;

namespace FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports;

/// <summary>
/// Report input parameters.
/// </summary>
/// <autogeneratedoc />
public class ReportParameter
{
    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTimeOffset StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
}