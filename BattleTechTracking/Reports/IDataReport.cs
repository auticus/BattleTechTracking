namespace BattleTechTracking.Reports
{
    public interface IDataReport<out T>
    {
        /// <summary>
        /// Generates the data necessary for the report
        /// </summary>
        /// <returns></returns>
        T GenerateReport(TextReportInput input);
    }
}
