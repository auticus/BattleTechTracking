using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DamageCodesView : ContentView
    {
        public static readonly BindableProperty DamageCodesProperty = BindableProperty.Create(
            nameof(DamageCodes),
            typeof(string),
            typeof(DamageCodesView));

        public static readonly BindableProperty OkCommandProperty = BindableProperty.Create(
            nameof(OkCommand),
            typeof(ICommand),
            typeof(DamageCodesView));

        public string DamageCodes
        {
            get => (string)GetValue(DamageCodesView.DamageCodesProperty);
            set => SetValue(DamageCodesView.DamageCodesProperty, value);
        }

        public Command OkCommand
        {
            get => (Command)GetValue(DamageCodesView.OkCommandProperty);
            set => SetValue(DamageCodesView.OkCommandProperty, value);
        }

        public DamageCodesView()
        {
            InitializeComponent();
        }
    }
}