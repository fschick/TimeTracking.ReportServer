namespace FS.TimeTracking.ReportServer.Abstractions.DTOs.Reports
{
    /// <summary>
    /// Activity report type
    /// </summary>
    public enum ActivityReportType
    {
        /// <summary>
        /// Generates a detailed activity report grouped by customer and project.
        /// </summary>
        Detailed,

        /// <summary>
        /// Generates an activity report grouped by customer, project and day.
        /// </summary>
        Daily,
    }
}
