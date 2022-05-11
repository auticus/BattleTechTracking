namespace BattleTechTracking.Reports
{
    public class ChartDefinition
    {
        public int[] RowHeights { get; }
        public int[] ColumnWidths { get; }

        public ChartDefinition(int[] rowWidths, int[] columnWidths)
        {
            RowHeights = rowWidths;
            ColumnWidths = columnWidths;
        }
    }
}
