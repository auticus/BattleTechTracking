using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleComponentView : ContentView
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            nameof(ItemSource),
            typeof(ObservableCollection<UnitComponent>),
            typeof(VehicleComponentView));

        public static readonly BindableProperty SelectedComponentProperty = BindableProperty.Create(
            nameof(SelectedComponent),
            typeof(UnitComponent),
            typeof(VehicleComponentView));

        public static readonly BindableProperty NewComponentProperty = BindableProperty.Create(
            nameof(NewComponent),
            typeof(ICommand),
            typeof(VehicleComponentView));

        public static readonly BindableProperty DeleteComponentProperty = BindableProperty.Create(
            nameof(DeleteComponent),
            typeof(ICommand),
            typeof(VehicleComponentView));

        public ObservableCollection<UnitComponent> ItemSource
        {
            get => (ObservableCollection<UnitComponent>)GetValue(VehicleComponentView.ItemSourceProperty);
            set => SetValue(VehicleComponentView.ItemSourceProperty, value);
        }

        public UnitComponent SelectedComponent
        {
            get => (UnitComponent)GetValue(VehicleComponentView.SelectedComponentProperty);
            set => SetValue(VehicleComponentView.SelectedComponentProperty, value);
        }

        public Command NewComponent
        {
            get => (Command)GetValue(VehicleComponentView.NewComponentProperty);
            set => SetValue(VehicleComponentView.NewComponentProperty, value);
        }

        public Command DeleteComponent
        {
            get => (Command)GetValue(VehicleComponentView.DeleteComponentProperty);
            set => SetValue(VehicleComponentView.DeleteComponentProperty, value);
        }

        public VehicleComponentView()
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