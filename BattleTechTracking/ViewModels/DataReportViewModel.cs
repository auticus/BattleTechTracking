using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Reports;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class DataReportViewModel : BaseViewModel
    {
        private string _selectedReport;
        private bool _textReportVisible;
        private bool _chartVisible;
        private string _textReportContents;
        private Grid _chartContents;
        private readonly Dictionary<string, dynamic> _reports;
        private TextReportInput _reportInput;

        public List<string> DataReports { get; } 

        public string SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                OnPropertyChanged(nameof(SelectedReport));
                DisplayAppropriatePanel();
                if (ChartVisible) GenerateChartContents();
            }
        }

        /// <summary>
        /// Gets or sets a value that will show the text-based reports.
        /// </summary>
        public bool TextReportVisible
        {
            get => _textReportVisible;
            set
            {
                _textReportVisible = value;
                OnPropertyChanged(nameof(TextReportVisible));
            }
        }

        /// <summary>
        /// Gets or sets a value that will show the charts.
        /// </summary>
        public bool ChartVisible
        {
            get => _chartVisible;
            set
            {
                _chartVisible = value;
                OnPropertyChanged(nameof(ChartVisible));
            }
        }

        /// <summary>
        /// Gets or sets the value that will populate the text report.
        /// </summary>
        public string TextReportContents
        {
            get => _textReportContents;
            set
            {
                _textReportContents = value;
                OnPropertyChanged(nameof(TextReportContents));
            }
        }

        /// <summary>
        /// Gets or sets the value that will populate the chart contents.
        /// </summary>
        public Grid ChartContents
        {
            get => _chartContents;
            set
            {
                _chartContents = value;
                OnPropertyChanged(nameof(ChartContents));
            }
        }

        /// <summary>
        /// Gets the command that will generate the report.
        /// </summary>
        public ICommand GenerateReportCommand { get; }
        
        public DataReportViewModel()
        {
            DataReports = DataReport.GetDataReportList().ToList();
            _reports = DataReport.GetDataReportDictionary();
            GenerateReportCommand = new Command(GenerateReport);
        }

        public void RefreshReportData(TextReportInput input)
        {
            _reportInput = input;
        }

        private void DisplayAppropriatePanel()
        {
            if (string.IsNullOrEmpty(SelectedReport))
            {
                TextReportVisible = false;
                ChartVisible = false;
                return;
            }

            var report = _reports[SelectedReport];
            TextReportVisible = (report is IDataReport);
            ChartVisible = (report is IChart);
        }

        private void GenerateChartContents()
        {
            var report = _reports[SelectedReport] as IChart;
            ChartContents = report?.GenerateChart();
        }

        private void GenerateReport()
        {
            if (string.IsNullOrEmpty(SelectedReport)) return;

            var report = _reports[SelectedReport];
            if (report is IDataReport dataReport)
            {
                TextReportContents = dataReport.GenerateReport(_reportInput);
            }
        }
    }
}
