using System.Collections.ObjectModel;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeaponsView : ContentView
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            nameof(ItemSource),
            typeof(ObservableCollection<Weapon>),
            typeof(WeaponsView));

        public static readonly BindableProperty SelectedWeaponProperty = BindableProperty.Create(
            nameof(SelectedWeapon),
            typeof(Weapon),
            typeof(WeaponsView));

        public static readonly BindableProperty NewWeaponProperty = BindableProperty.Create(
            nameof(NewWeapon),
            typeof(ICommand),
            typeof(WeaponsView));

        public static readonly BindableProperty DeleteWeaponProperty = BindableProperty.Create(
            nameof(DeleteWeapon),
            typeof(ICommand),
            typeof(WeaponsView));

        public static readonly BindableProperty OpenDamageCodesProperty = BindableProperty.Create(
            nameof(OpenDamageCodes),
            typeof(ICommand),
            typeof(WeaponsView)
        );

        public static readonly BindableProperty OpenAmmoProperty = BindableProperty.Create(
            nameof(OpenAmmo),
            typeof(ICommand),
            typeof(WeaponsView)
        );

        public ObservableCollection<Weapon> ItemSource
        {
            get => (ObservableCollection<Weapon>)GetValue(WeaponsView.ItemSourceProperty);
            set => SetValue(WeaponsView.ItemSourceProperty, value);
        }

        public Weapon SelectedWeapon
        {
            get => (Weapon)GetValue(WeaponsView.SelectedWeaponProperty);
            set => SetValue(WeaponsView.SelectedWeaponProperty, value);
        }

        public Command NewWeapon
        {
            get => (Command)GetValue(WeaponsView.NewWeaponProperty);
            set => SetValue(WeaponsView.NewWeaponProperty, value);
        }

        public Command DeleteWeapon
        {
            get => (Command)GetValue(WeaponsView.DeleteWeaponProperty);
            set => SetValue(WeaponsView.DeleteWeaponProperty, value);
        }

        public Command OpenDamageCodes
        {
            get => (Command)GetValue(WeaponsView.OpenDamageCodesProperty);
            set => SetValue(WeaponsView.OpenDamageCodesProperty, value);
        }
        public Command OpenAmmo
        {
            get => (Command)GetValue(WeaponsView.OpenAmmoProperty);
            set => SetValue(WeaponsView.OpenAmmoProperty, value);
        }

        public WeaponsView()
        {
            InitializeComponent();
        }
    }
}