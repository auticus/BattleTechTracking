namespace BattleTechTracking.Reports
{
    public interface IDataReport
    {
        /// <summary>
        /// Generates the data necessary for the report
        /// </summary>
        /// <returns></returns>
        string GenerateReport(TextReportInput input);
    }
}
