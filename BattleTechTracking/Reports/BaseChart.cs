using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public abstract class BaseChart : IChart
    {
        public abstract Grid GenerateChart();

        /// <summary>
        /// Generates the beginning xaml structure for a grid based off of the definitions passed to it.
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        protected Grid GenerateGridAndRowColumnDefinitions(ChartDefinition chart)
        {
            var grid = new Grid();
            foreach (var height in chart.RowHeights)
            {
                grid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(height)});
            }

            foreach (var width in chart.ColumnWidths)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition(){Width= new GridLength(width)});
            }

            return grid;
        }

        protected void InjectChartHeader(Grid grid, string caption, int columnSpan, Color textColor)
        {
            var boxView = new BoxView { Color = Color.MidnightBlue};
            Grid.SetRow(boxView, 0);
            Grid.SetColumnSpan(boxView, columnSpan);

            var label = new Label
            {
                Text = caption,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = textColor,
                FontAttributes = FontAttributes.Bold
            };

            Grid.SetRow(label, 0);
            Grid.SetColumnSpan(label, columnSpan);

            grid.Children.Add(boxView);
            grid.Children.Add(label);
        }

        protected void InjectLabelInColumn(Grid grid, string caption, int row, int column, Color textColor)
        {
            InjectLabelInColumn(grid, caption, row, column, textColor, columnSpan: 1);
        }

        protected void InjectLabelInColumn(Grid grid, string caption, int row, int column, Color textColor, int columnSpan)
        {
            var rowColor = Color.Black;
            if (row % 2 == 0)
            {
                rowColor = Color.DimGray;
            }

            var boxView = new BoxView { Color = rowColor };
            Grid.SetRow(boxView, row);
            Grid.SetColumn(boxView, column);
            if (columnSpan > 1) Grid.SetColumnSpan(boxView, columnSpan);

            var label = new Label
            {
                Text = caption,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = textColor
            };

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            if (columnSpan > 1) Grid.SetColumnSpan(label, columnSpan);

            grid.Children.Add(boxView);
            grid.Children.Add(label);
        }

        protected void InjectLabelInFooter(Grid grid, string caption, int row, Color textColor, int columnSpan)
        {
            var rowColor = Color.Black;
            var boxView = new BoxView { Color = rowColor };
            Grid.SetRow(boxView, row);
            Grid.SetColumnSpan(boxView, columnSpan);

            var label = new Label
            {
                Text = caption,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = textColor,
                FontAttributes = FontAttributes.Italic
            };

            Grid.SetRow(label, row);
            Grid.SetColumnSpan(label, columnSpan);

            grid.Children.Add(boxView);
            grid.Children.Add(label);
        }
    }
}
