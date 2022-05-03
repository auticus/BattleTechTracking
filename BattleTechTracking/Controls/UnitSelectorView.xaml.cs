using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitSelectorView : ContentView
    {
        public static readonly BindableProperty UnitFiltersProperty = BindableProperty.Create(
            nameof(UnitFilters),
            typeof(ObservableCollection<string>),
            typeof(UnitSelectorView));

        public static readonly BindableProperty SelectedUnitFilterProperty = BindableProperty.Create(
            nameof(SelectedUnitFilter),
            typeof(string),
            typeof(UnitSelectorView));

        public static readonly BindableProperty UnitNameFilterProperty = BindableProperty.Create(
            nameof(UnitNameFilter),
            typeof(string),
            typeof(UnitSelectorView));

        public static readonly BindableProperty SelectedUnitProperty = BindableProperty.Create(
            nameof(SelectedUnit),
            typeof(IDisplayListView),
            typeof(UnitSelectorView));

        public static readonly BindableProperty SelectorOkProperty = BindableProperty.Create(
            nameof(SelectorOk),
            typeof(ICommand),
            typeof(UnitSelectorView));

        public static readonly BindableProperty VisibleUnitsProperty = BindableProperty.Create(
            nameof(VisibleUnits),
            typeof(ObservableCollection<IDisplayListView>),
            typeof(UnitSelectorView));

        public static readonly BindableProperty FilterUnitsProperty = BindableProperty.Create(
            nameof(FilterUnits),
            typeof(ICommand),
            typeof(UnitSelectorView));

        public ObservableCollection<IDisplayListView> VisibleUnits
        {
            get => (ObservableCollection<IDisplayListView>)GetValue(UnitSelectorView.VisibleUnitsProperty);
            set => SetValue(UnitSelectorView.VisibleUnitsProperty, value);
        }

        public IDisplayListView SelectedUnit
        {
            get => (IDisplayListView)GetValue(UnitSelectorView.SelectedUnitProperty);
            set => SetValue(UnitSelectorView.SelectedUnitProperty, value);
        }

        public ObservableCollection<string> UnitFilters
        {
            get => (ObservableCollection<string>)GetValue(UnitSelectorView.UnitFiltersProperty);
            set => SetValue(UnitSelectorView.UnitFiltersProperty, value);
        }
        
        public string SelectedUnitFilter
        {
            get => (string)GetValue(UnitSelectorView.SelectedUnitFilterProperty);
            set => SetValue(UnitSelectorView.SelectedUnitFilterProperty, value);
        }

        public string UnitNameFilter
        {
            get => (string)GetValue(UnitSelectorView.UnitNameFilterProperty);
            set => SetValue(UnitSelectorView.UnitNameFilterProperty, value);
        }

        public Command SelectorOk
        {
            get => (Command)GetValue(UnitSelectorView.SelectorOkProperty);
            set => SetValue(UnitSelectorView.SelectorOkProperty, value);
        }

        public Command FilterUnits
        {
            get => (Command)GetValue(UnitSelectorView.FilterUnitsProperty);
            set => SetValue(UnitSelectorView.FilterUnitsProperty, value);
        }
        public UnitSelectorView()
        {
            InitializeComponent();
        }
    }
}