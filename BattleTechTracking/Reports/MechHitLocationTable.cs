using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class MechHitLocationTable : BaseChart
    {
        private const int FULL_COL_SPAN = 4;

        public MechHitLocationTable()
        {
            LoadEntries();
        }

        /// <inheritdoc/>
        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "HIT LOCATION TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2", "Left Torso(*)", "Center Torso(*)", "Right Torso(*)" });
            ChartEntries.Add(new[] { "3", "Left Leg", "Right Arm", "Right Leg" });
            ChartEntries.Add(new []{ "4", "Left Arm", "Right Arm", "Right Arm" });
            ChartEntries.Add(new[] { "5", "Left Arm", "Right Leg", "Right Arm" });
            ChartEntries.Add(new[] { "6", "Left Leg", "Right Torso", "Right Leg" });
            ChartEntries.Add(new[] { "7", "Left Torso", "Center Torso", "Right Torso" });
            ChartEntries.Add(new[] { "8", "Center Torso", "Left Torso", "Center Torso" });
            ChartEntries.Add(new[] { "9", "Right Torso", "Left Leg", "Left Torso" });
            ChartEntries.Add(new[] { "10", "Right Arm", "Left Arm", "Left Arm" });
            ChartEntries.Add(new[] { "11", "Right Leg", "Left Arm", "Left Leg" });
            ChartEntries.Add(new[] { "12", "Head", "Head", "Head" });

        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 50 };
            var cols = new[] { 100, 100, 100, 100 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Left Side", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Front/Rear", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Right Side", row, col, Color.Gold);
        }

        private void InjectFooter(Grid grid)
        {
            const int row = 13;
            InjectLabelInFooter(grid, "A result of 2 may inflict a crit.  Apply damage as normal but roll once on the Determining Crits table.",
                row,
                Color.Gold,
                FULL_COL_SPAN);
        }
    }
}
