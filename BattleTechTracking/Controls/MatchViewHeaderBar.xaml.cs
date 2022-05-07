using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchViewHeaderBar : ContentView
    {
        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
            nameof(CloseCommand),
            typeof(ICommand),
            typeof(MatchViewHeaderBar));

        public static readonly BindableProperty SaveCommandProperty = BindableProperty.Create(
            nameof(SaveCommand),
            typeof(ICommand),
            typeof(MatchViewHeaderBar));

        public static readonly BindableProperty SettingsCommandProperty = BindableProperty.Create(
            nameof(SettingsCommand),
            typeof(ICommand),
            typeof(MatchViewHeaderBar));

        public static readonly BindableProperty NewRoundCommandProperty = BindableProperty.Create(
            nameof(NewRoundCommand),
            typeof(ICommand),
            typeof(MatchViewHeaderBar));

        public static readonly BindableProperty ReportsCommandProperty = BindableProperty.Create(
            nameof(ReportsCommand),
            typeof(ICommand),
            typeof(MatchViewHeaderBar));

        public Command CloseCommand
        {
            get => (Command)GetValue(MatchViewHeaderBar.CloseCommandProperty);
            set => SetValue(MatchViewHeaderBar.CloseCommandProperty, value);
        }

        public Command SaveCommand
        {
            get => (Command)GetValue(MatchViewHeaderBar.SaveCommandProperty);
            set => SetValue(MatchViewHeaderBar.SaveCommandProperty, value);
        }

        public Command NewRoundCommand
        {
            get => (Command)GetValue(MatchViewHeaderBar.NewRoundCommandProperty);
            set => SetValue(MatchViewHeaderBar.NewRoundCommandProperty, value);
        }

        public Command SettingsCommand
        {
            get => (Command)GetValue(MatchViewHeaderBar.SettingsCommandProperty);
            set => SetValue(MatchViewHeaderBar.SettingsCommandProperty, value);
        }

        public Command ReportsCommand
        {
            get => (Command)GetValue(MatchViewHeaderBar.ReportsCommandProperty);
            set => SetValue(MatchViewHeaderBar.ReportsCommandProperty, value);
        }

        public MatchViewHeaderBar()
        {
            InitializeComponent();
        }
    }
}