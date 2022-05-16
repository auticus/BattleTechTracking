using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileLoadView : ContentView
    {
        private string _selectedFileName;

        public static readonly BindableProperty OkButtonProperty = BindableProperty.Create(
            nameof(OkButton),
            typeof(ICommand),
            typeof(FileLoadView));

        public static readonly BindableProperty CancelButtonProperty = BindableProperty.Create(
            nameof(CancelButton),
            typeof(ICommand),
            typeof(FileLoadView));

        public static readonly BindableProperty DeleteButtonProperty = BindableProperty.Create(
            nameof(DeleteButton),
            typeof(ICommand),
            typeof(FileLoadView));

        public static readonly BindableProperty FileNameProperty = BindableProperty.Create(
            nameof(FileName),
            typeof(string),
            typeof(FileLoadView));

        public static readonly BindableProperty FileNamesProperty = BindableProperty.Create(
            nameof(FileNames),
            typeof(IEnumerable<string>),
            typeof(FileLoadView));

        public static readonly BindableProperty IsLoadFileProperty = BindableProperty.Create(
            nameof(IsLoadFile),
            typeof(bool),
            typeof(FileLoadView));

        public string FileName
        {
            get => (string)GetValue(FileLoadView.FileNameProperty);
            set => SetValue(FileLoadView.FileNameProperty, value);
        }

        public bool IsLoadFile
        {
            get => (bool)GetValue(FileLoadView.IsLoadFileProperty);
            set => SetValue(FileLoadView.IsLoadFileProperty, value);
        }
        

        public ICommand OkButton
        {
            get => (ICommand)GetValue(FileLoadView.OkButtonProperty);
            set => SetValue(FileLoadView.OkButtonProperty, value);
        }

        public ICommand CancelButton
        {
            get => (ICommand)GetValue(FileLoadView.CancelButtonProperty);
            set => SetValue(FileLoadView.CancelButtonProperty, value);
        }

        public ICommand DeleteButton
        {
            get => (ICommand)GetValue(FileLoadView.DeleteButtonProperty);
            set => SetValue(FileLoadView.DeleteButtonProperty, value);
        }

        public IEnumerable<string> FileNames
        {
            get => (IEnumerable<string>)GetValue(FileLoadView.FileNamesProperty);
            set => SetValue(FileLoadView.FileNamesProperty, value);
        }

        public string SelectedFileName
        {
            get => _selectedFileName;
            set
            {
                _selectedFileName = value;
                FileName = _selectedFileName;
                OnPropertyChanged(nameof(SelectedFileName));
            }
        }

        public FileLoadView()
        {
            InitializeComponent();
        }
    }
}