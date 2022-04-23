using System.Collections.Generic;
using System.Windows.Input;
using BattleTechTracking.Models;
using BattleTechTracking.Reports;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class DataReportViewModel : BaseViewModel
    {
        private string _selectedReport;
        private bool _textReportVisible;
        private string _textReportContents;
        private readonly Dictionary<string, dynamic> _reports = new Dictionary<string, dynamic>();
        private TextReportInput _reportInput;

        public List<string> DataReports { get; } = new List<string>();

        public string SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                OnPropertyChanged(nameof(SelectedReport));
                HideAllReportPanels();
                DisplayAppropriatePanel();
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
        /// Gets the command that will generate the report.
        /// </summary>
        public ICommand GenerateReportCommand { get; }
        
        public DataReportViewModel()
        {
            BuildReportList();
            BuildReportDictionary();
            GenerateReportCommand = new Command(GenerateReport);
        }

        public void RefreshReportData(TextReportInput input)
        {
            _reportInput = input;
        }

        private void BuildReportList()
        {
            DataReports.Add(DataReport.DAMAGE_REPORT);
        }

        private void BuildReportDictionary()
        {
            _reports.Add(DataReport.DAMAGE_REPORT, new DamageReport());
        }

        private void HideAllReportPanels()
        {
            TextReportVisible = false;
        }

        private void DisplayAppropriatePanel()
        {
            // currently this is the only type of report but charts will be added in the future
            TextReportVisible = true;
        }

        private void GenerateReport()
        {
            if (string.IsNullOrEmpty(SelectedReport)) return;

            var report = _reports[SelectedReport];
            if (report is IDataReport<string> dataReport)
            {
                TextReportContents = dataReport.GenerateReport(_reportInput);
            }
        }
    }
}
