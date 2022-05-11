using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class CombatVehicleMotiveDamageTable : BaseChart
    {
        private const int FULL_COL_SPAN = 2;

        public CombatVehicleMotiveDamageTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "MOTIVE SYSTEM DAMAGE TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2-5", "No Effect" });
            ChartEntries.Add(new[] { "6-7", "Minor Damage; +1 driving skill rolls" });
            ChartEntries.Add(new[] { "8-9", "Moderate Damage; -1 MP;+2 drive skill rolls" });
            ChartEntries.Add(new[] { "10-11", "Heavy Damage; half MP;+3 drive skill rolls" });
            ChartEntries.Add(new[] { "12", "Immobilized" });
            ChartEntries.Add(new[] { "", "Hit Came from Rear +1" });
            ChartEntries.Add(new[] { "", "Hit came from sides +2" });
            ChartEntries.Add(new[] { "", "Tracked, Naval +0" });
            ChartEntries.Add(new[] { "", "Wheeled +2" });
            ChartEntries.Add(new[] { "", "Hovercraft +3" });
            ChartEntries.Add(new[] { "", "WiGE +4" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 100, 350 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Effect", row, col, Color.Gold);
        }

        private void InjectFooter(Grid grid)
        {
            const int row = 13;
            InjectLabelInFooter(grid, "All move and drive skill penalties are cumulative.",
                row,
                Color.Gold,
                FULL_COL_SPAN);
        }
    }
}
