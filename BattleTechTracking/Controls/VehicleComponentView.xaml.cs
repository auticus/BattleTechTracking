using System.Collections.ObjectModel;
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

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(UnitComponent),
            typeof(VehicleComponentView));

        public ObservableCollection<UnitComponent> ItemSource
        {
            get => (ObservableCollection<UnitComponent>)GetValue(VehicleComponentView.ItemSourceProperty);
            set => SetValue(VehicleComponentView.ItemSourceProperty, value);
        }

        public UnitComponent SelectedItem
        {
            get => (UnitComponent)GetValue(VehicleComponentView.SelectedItemProperty);
            set => SetValue(VehicleComponentView.SelectedItemProperty, value);
        }

        public VehicleComponentView()
        {
            InitializeComponent();
        }
    }
}