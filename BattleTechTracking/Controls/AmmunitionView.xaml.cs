using System.Collections.ObjectModel;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmmunitionView : ContentView
    {
        public static readonly BindableProperty AmmunitionListProperty = BindableProperty.Create(
            nameof(AmmunitionList),
            typeof(ObservableCollection<Ammunition>),
            typeof(AmmunitionView));

        public static readonly BindableProperty SelectedAmmoProperty = BindableProperty.Create(
            nameof(SelectedAmmo),
            typeof(Ammunition),
            typeof(AmmunitionView));

        public static readonly BindableProperty OkCommandProperty = BindableProperty.Create(
            nameof(OkCommand),
            typeof(ICommand),
            typeof(DamageCodesView));

        public static readonly BindableProperty NewAmmoProperty = BindableProperty.Create(
            nameof(NewAmmo),
            typeof(ICommand),
            typeof(AmmunitionView));

        public static readonly BindableProperty DeleteAmmoProperty = BindableProperty.Create(
            nameof(DeleteAmmo),
            typeof(ICommand),
            typeof(AmmunitionView));

        public static readonly BindableProperty AmmunitionViewHeaderProperty = BindableProperty.Create(
            nameof(AmmunitionViewHeader),
            typeof(string),
            typeof(AmmunitionView)
        );

        public ObservableCollection<Ammunition> AmmunitionList
        {
            get => (ObservableCollection<Ammunition>)GetValue(AmmunitionView.AmmunitionListProperty);
            set => SetValue(AmmunitionView.AmmunitionListProperty, value);
        }

        public Ammunition SelectedAmmo
        {
            get => (Ammunition)GetValue(AmmunitionView.SelectedAmmoProperty);
            set => SetValue(AmmunitionView.SelectedAmmoProperty, value);
        }

        public Command OkCommand
        {
            get => (Command)GetValue(AmmunitionView.OkCommandProperty);
            set => SetValue(AmmunitionView.NewAmmoProperty, value);
        }

        public Command NewAmmo
        {
            get => (Command)GetValue(AmmunitionView.NewAmmoProperty);
            set => SetValue(AmmunitionView.NewAmmoProperty, value);
        }

        public Command DeleteAmmo
        {
            get => (Command)GetValue(AmmunitionView.DeleteAmmoProperty);
            set => SetValue(AmmunitionView.DeleteAmmoProperty, value);
        }

        public string AmmunitionViewHeader
        {
            get => (string)GetValue(AmmunitionView.AmmunitionViewHeaderProperty);
            set => SetValue(AmmunitionView.AmmunitionViewHeaderProperty, value);
        }

        public AmmunitionView()
        {
            InitializeComponent();
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            var textBox = sender as Entry;
            if (textBox?.Text == null) return;

            textBox.CursorPosition = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }
    }
}