using System.Collections.Generic;
using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class MechHitLocationTable : BaseChart
    {
        private const int FULL_COL_SPAN = 4;
        private readonly List<string[]> _chartEntries = new List<string[]>();

        public MechHitLocationTable()
        {
            LoadEntries();
        }

        /// <inheritdoc/>
        public override Grid GenerateChart()
        {
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "HIT LOCATION TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);

            var row = 2;
            var col = 0;
            foreach (var rowEntry in _chartEntries)
            {
                foreach (var entry in rowEntry)
                {
                    InjectLabelInColumn(grid, entry, row, col, Color.WhiteSmoke);
                    col++;
                }

                col = 0;
                row++;
            }

            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            _chartEntries.Add(new[] { "2", "Left Torso(*)", "Center Torso(*)", "Right Torso(*)" });
            _chartEntries.Add(new[] { "3", "Left Leg", "Right Arm", "Right Leg" });
            _chartEntries.Add(new []{ "4", "Left Arm", "Right Arm", "Right Arm" });
            _chartEntries.Add(new[] { "5", "Left Arm", "Right Leg", "Right Arm" });
            _chartEntries.Add(new[] { "6", "Left Leg", "Right Torso", "Right Leg" });
            _chartEntries.Add(new[] { "7", "Left Torso", "Center Torso", "Right Torso" });
            _chartEntries.Add(new[] { "8", "Center Torso", "Left Torso", "Center Torso" });
            _chartEntries.Add(new[] { "9", "Right Torso", "Left Leg", "Left Torso" });
            _chartEntries.Add(new[] { "10", "Right Arm", "Left Arm", "Left Arm" });
            _chartEntries.Add(new[] { "11", "Right Leg", "Left Arm", "Left Leg" });
            _chartEntries.Add(new[] { "12", "Head", "Head", "Head" });

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
