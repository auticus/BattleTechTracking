using System.Collections.ObjectModel;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentView : ContentView
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            nameof(ItemSource),
            typeof(ObservableCollection<Equipment>),
            typeof(EquipmentView));

        public static readonly BindableProperty SelectedEquipmentProperty = BindableProperty.Create(
            nameof(SelectedEquipment),
            typeof(Equipment),
            typeof(EquipmentView));

        public static readonly BindableProperty NewEquipmentProperty = BindableProperty.Create(
            nameof(NewEquipment),
            typeof(ICommand),
            typeof(EquipmentView));

        public static readonly BindableProperty DeleteEquipmentProperty = BindableProperty.Create(
            nameof(DeleteEquipment),
            typeof(ICommand),
            typeof(EquipmentView));

        public static readonly BindableProperty CopyEquipmentProperty = BindableProperty.Create(
            nameof(CopyEquipment),
            typeof(ICommand),
            typeof(EquipmentView));

        public static readonly BindableProperty IsDataEditModeProperty = BindableProperty.Create(
            nameof(IsDataEditMode),
            typeof(bool),
            typeof(EquipmentView));

        public ObservableCollection<Equipment> ItemSource
        {
            get => (ObservableCollection<Equipment>)GetValue(EquipmentView.ItemSourceProperty);
            set => SetValue(EquipmentView.ItemSourceProperty, value);
        }

        public Equipment SelectedEquipment
        {
            get => (Equipment)GetValue(EquipmentView.SelectedEquipmentProperty);
            set => SetValue(EquipmentView.SelectedEquipmentProperty, value);
        }

        public Command NewEquipment
        {
            get => (Command)GetValue(EquipmentView.NewEquipmentProperty);
            set => SetValue(EquipmentView.NewEquipmentProperty, value);
        }

        public Command DeleteEquipment
        {
            get => (Command)GetValue(EquipmentView.DeleteEquipmentProperty);
            set => SetValue(EquipmentView.DeleteEquipmentProperty, value);
        }

        public Command CopyEquipment
        {
            get => (Command)GetValue(EquipmentView.CopyEquipmentProperty);
            set => SetValue(EquipmentView.CopyEquipmentProperty, value);
        }

        public bool IsDataEditMode
        {
            get => (bool)GetValue(EquipmentView.IsDataEditModeProperty);
            set => SetValue(EquipmentView.IsDataEditModeProperty, value);
        }

        public EquipmentView()
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