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

        public static readonly BindableProperty TemplatedWeaponsProperty = BindableProperty.Create(
            nameof(TemplatedWeapons),
            typeof(ObservableCollection<Weapon>),
            typeof(WeaponsView));

        public static readonly BindableProperty SelectedWeaponProperty = BindableProperty.Create(
            nameof(SelectedWeapon),
            typeof(Weapon),
            typeof(WeaponsView));

        public static readonly BindableProperty SelectedTemplatedWeaponProperty = BindableProperty.Create(
            nameof(SelectedTemplatedWeapon),
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

        public static readonly BindableProperty CopyWeaponProperty = BindableProperty.Create(
            nameof(CopyWeapon),
            typeof(ICommand),
            typeof(WeaponsView));

        public static readonly BindableProperty OpenWeaponTemplateProperty = BindableProperty.Create(
            nameof(OpenWeaponTemplate),
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
            typeof(WeaponsView));

        public static readonly BindableProperty IsDataEditModeProperty = BindableProperty.Create(
            nameof(IsDataEditMode),
            typeof(bool),
            typeof(WeaponsView));

        public static readonly BindableProperty HitsProperty = BindableProperty.Create(
            nameof(Hits),
            typeof(int),
            typeof(WeaponsView));

        public static readonly BindableProperty FireWeaponCommandProperty = BindableProperty.Create(
            nameof(FireWeaponCommand),
            typeof(ICommand),
            typeof(WeaponsView));

        public static readonly BindableProperty TargetedElementsProperty = BindableProperty.Create(
            nameof(TargetedElements),
            typeof(ObservableCollection<TargetedEntity>),
            typeof(WeaponsView));

        public static readonly BindableProperty SelectedTargetedElementProperty = BindableProperty.Create(
            nameof(SelectedTargetedElement),
            typeof(TargetedEntity),
            typeof(WeaponsView));

        public static readonly BindableProperty SensorsDamagedProperty = BindableProperty.Create(
            nameof(SensorsDamaged),
            typeof(bool),
            typeof(WeaponsView));

        public static readonly BindableProperty ArmShoulderDamagedProperty = BindableProperty.Create(
            nameof(ArmShoulderDamaged),
            typeof(bool),
            typeof(WeaponsView));

        public ObservableCollection<Weapon> ItemSource
        {
            get => (ObservableCollection<Weapon>)GetValue(WeaponsView.ItemSourceProperty);
            set => SetValue(WeaponsView.ItemSourceProperty, value);
        }

        public ObservableCollection<Weapon> TemplatedWeapons
        {
            get => (ObservableCollection<Weapon>)GetValue(WeaponsView.TemplatedWeaponsProperty);
            set => SetValue(WeaponsView.TemplatedWeaponsProperty, value);
        }
    
        public Weapon SelectedWeapon
        {
            get => (Weapon)GetValue(WeaponsView.SelectedWeaponProperty);
            set => SetValue(WeaponsView.SelectedWeaponProperty, value);
        }

        public Weapon SelectedTemplatedWeapon
        {
            get => (Weapon)GetValue(WeaponsView.SelectedTemplatedWeaponProperty);
            set => SetValue(WeaponsView.SelectedTemplatedWeaponProperty, value);
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

        public Command CopyWeapon
        {
            get => (Command)GetValue(WeaponsView.CopyWeaponProperty);
            set => SetValue(WeaponsView.CopyWeaponProperty, value);
        }

        public Command OpenWeaponTemplate
        {
            get => (Command)GetValue(WeaponsView.OpenWeaponTemplateProperty);
            set => SetValue(WeaponsView.OpenWeaponTemplateProperty, value);
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

        public Command FireWeaponCommand
        {
            get => (Command)GetValue(WeaponsView.FireWeaponCommandProperty);
            set => SetValue(WeaponsView.FireWeaponCommandProperty, value);
        }

        public bool IsDataEditMode
        {
            get => (bool)GetValue(WeaponsView.IsDataEditModeProperty);
            set => SetValue(WeaponsView.IsDataEditModeProperty, value);
        }

        public int Hits
        {
            get => (int)GetValue(WeaponsView.HitsProperty);
            set => SetValue(WeaponsView.HitsProperty, value);
        }

        public ObservableCollection<TargetedEntity> TargetedElements
        {
            get => (ObservableCollection<TargetedEntity>)GetValue(WeaponsView.TargetedElementsProperty);
            set => SetValue(WeaponsView.TargetedElementsProperty, value);
        }

        public TargetedEntity SelectedTargetedElement
        {
            get => (TargetedEntity)GetValue(WeaponsView.SelectedTargetedElementProperty);
            set => SetValue(WeaponsView.SelectedTargetedElementProperty, value);
        }

        public bool SensorsDamaged
        {
            get => (bool)GetValue(WeaponsView.SensorsDamagedProperty);
            set => SetValue(WeaponsView.SensorsDamagedProperty, value);
        }

        public bool ArmShoulderDamaged
        {
            get => (bool)GetValue(WeaponsView.ArmShoulderDamagedProperty);
            set => SetValue(WeaponsView.ArmShoulderDamagedProperty, value);
        }

        public WeaponsView()
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