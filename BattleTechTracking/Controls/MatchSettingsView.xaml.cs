using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchSettingsView : ContentView
    {
        public static readonly BindableProperty Faction1NameProperty = BindableProperty.Create(
            nameof(Faction1Name),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty Faction2NameProperty = BindableProperty.Create(
            nameof(Faction2Name),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty SettingsOkProperty = BindableProperty.Create(
            nameof(SettingsOk),
            typeof(ICommand),
            typeof(MatchSettingsView));

        public string Faction1Name
        {
            get => (string)GetValue(MatchSettingsView.Faction1NameProperty);
            set => SetValue(MatchSettingsView.Faction1NameProperty, value);
        }

        public string Faction2Name
        {
            get => (string)GetValue(MatchSettingsView.Faction2NameProperty);
            set => SetValue(MatchSettingsView.Faction2NameProperty, value);
        }

        public Command SettingsOk
        {
            get => (Command)GetValue(MatchSettingsView.SettingsOkProperty);
            set => SetValue(MatchSettingsView.SettingsOkProperty, value);
        }

        public MatchSettingsView()
        {
            InitializeComponent();
        }
    }
}