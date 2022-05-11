using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsView : ContentView
    {
        public static readonly BindableProperty ReportsProperty = BindableProperty.Create(
            nameof(Reports),
            typeof(List<string>),
            typeof(ReportsView));

        public static readonly BindableProperty SelectedReportProperty = BindableProperty.Create(
            nameof(SelectedReport),
            typeof(string),
            typeof(ReportsView));

        public static readonly BindableProperty TextReportVisibleProperty = BindableProperty.Create(
            nameof(TextReportVisible),
            typeof(bool),
            typeof(ReportsView)
            );

        public static readonly BindableProperty ChartVisibleProperty = BindableProperty.Create(
            nameof(ChartVisible),
            typeof(bool),
            typeof(ReportsView)
        );

        public static readonly BindableProperty TextReportContentsProperty = BindableProperty.Create(
            nameof(TextReportContents),
            typeof(string),
            typeof(ReportsView));

        public static readonly BindableProperty ChartContentsProperty = BindableProperty.Create(
            nameof(ChartContents),
            typeof(Grid),
            typeof(ReportsView));

        public static readonly BindableProperty GenerateReportProperty = BindableProperty.Create(
            nameof(GenerateReport),
            typeof(ICommand),
            typeof(ReportsView));

        public List<string> Reports
        {
            get => (List<string>)GetValue(ReportsView.ReportsProperty);
            set => SetValue(ReportsView.ReportsProperty, value);
        }

        public string SelectedReport
        {
            get => (string)GetValue(ReportsView.SelectedReportProperty);
            set => SetValue(ReportsView.SelectedReportProperty, value);
        }

        public bool TextReportVisible
        {
            get => (bool)GetValue(ReportsView.TextReportVisibleProperty);
            set => SetValue(ReportsView.TextReportVisibleProperty, value);
        }

        public bool ChartVisible
        {
            get => (bool)GetValue(ReportsView.ChartVisibleProperty);
            set => SetValue(ReportsView.ChartVisibleProperty, value);
        }

        public string TextReportContents
        {
            get => (string)GetValue(ReportsView.TextReportContentsProperty);
            set => SetValue(ReportsView.TextReportContentsProperty, value);
        }

        /// <summary>
        /// XAML contents that will be dynamically rendered into a visual element.
        /// </summary>
        public Grid ChartContents
        {
            get
            {
                var grid = (Grid)GetValue(ReportsView.ChartContentsProperty);
                ChartLayout.Children.Clear();

                if (grid == null) return null;

                ChartLayout.Children.Add(grid);
                return grid;
            } 
            set => SetValue(ReportsView.ChartContentsProperty, value);
        }

        public Command GenerateReport
        {
            get => (Command)GetValue(ReportsView.GenerateReportProperty);
            set => SetValue(ReportsView.GenerateReportProperty, value);
        }

        public ReportsView()
        {
            InitializeComponent();
        }
    }
}