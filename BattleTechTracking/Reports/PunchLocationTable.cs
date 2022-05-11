using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class PunchLocationTable : BaseChart
    {
        private const int FULL_COL_SPAN = 4;

        public PunchLocationTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "PUNCH LOCATION TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            return grid;
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (1d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Left Side", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Front/Rear", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Right Side", row, col, Color.Gold);
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "1", "Left Torso", "Left Arm", "Right Torso" });
            ChartEntries.Add(new[] { "2", "Left Torso", "Left Torso", "Right Torso" });
            ChartEntries.Add(new[] { "3", "Center Torso", "Center Torso", "Center Torso" });
            ChartEntries.Add(new[] { "4", "Left Arm", "Right Torso", "Right Arm" });
            ChartEntries.Add(new[] { "5", "Left Arm", "Right Arm", "Right Arm" });
            ChartEntries.Add(new[] { "6", "Head", "Head", "Head" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 150, 150, 150, 150};
            return new ChartDefinition(rows, cols);
        }
    }
}
