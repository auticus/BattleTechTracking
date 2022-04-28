using System.Collections.Generic;
using System.Windows.Input;
using BattleTechTracking.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackedGameElementView : ContentView
    {
        public static readonly BindableProperty TrackedElementHeaderProperty = BindableProperty.Create(
            nameof(TrackedElementHeader),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty UnitActionsProperty = BindableProperty.Create(
            nameof(UnitActions),
            typeof(List<string>),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty UnitStatusesProperty = BindableProperty.Create(
            nameof(UnitStatuses),
            typeof(List<string>),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty SelectedUnitActionProperty = BindableProperty.Create(
            nameof(SelectedUnitAction),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty SelectedUnitStatusProperty = BindableProperty.Create(
            nameof(SelectedUnitStatus),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty QuirksProperty = BindableProperty.Create(
            nameof(Quirks),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty DidWalkProperty = BindableProperty.Create(
            nameof(DidWalk),
            typeof(bool),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty DidRunProperty = BindableProperty.Create(
            nameof(DidRun),
            typeof(bool),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty DidJumpProperty = BindableProperty.Create(
            nameof(DidJump),
            typeof(bool),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty IsProneProperty = BindableProperty.Create(
            nameof(IsProne),
            typeof(bool),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty CurrentHeatLevelProperty = BindableProperty.Create(
            nameof(CurrentHeatLevel),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty CurrentHeatSinksProperty = BindableProperty.Create(
            nameof(CurrentHeatSinks),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty HexesMovedProperty = BindableProperty.Create(
            nameof(HexesMoved),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty NumberOfElementsProperty = BindableProperty.Create(
            nameof(NumberOfElements),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty PilotNameProperty = BindableProperty.Create(
            nameof(PilotName),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty PilotSkillProperty = BindableProperty.Create(
            nameof(PilotSkill),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty PilotGunnerySkillProperty = BindableProperty.Create(
            nameof(PilotGunnerySkill),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty PilotHitsProperty = BindableProperty.Create(
            nameof(PilotHits),
            typeof(int),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty NotesProperty = BindableProperty.Create(
            nameof(Notes),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty MovementProperty = BindableProperty.Create(
            nameof(Movement),
            typeof(string),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty Heat1CommandProperty = BindableProperty.Create(
            nameof(Heat1Command),
            typeof(ICommand),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty Heat2CommandProperty = BindableProperty.Create(
            nameof(Heat2Command),
            typeof(ICommand),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty Heat5CommandProperty = BindableProperty.Create(
            nameof(Heat5Command),
            typeof(ICommand),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty TrackHeatProperty = BindableProperty.Create(
            nameof(TrackHeat),
            typeof(bool),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty HeatColorLevelProperty = BindableProperty.Create(
            nameof(HeatColorLevel),
            typeof(HeatLevels),
            typeof(TrackedGameElementView));

        public static readonly BindableProperty HeatToolTipProperty = BindableProperty.Create(
            nameof(HeatToolTip),
            typeof(string),
            typeof(TrackedGameElementView));

        public string TrackedElementHeader
        {
            get => (string)GetValue(TrackedGameElementView.TrackedElementHeaderProperty);
            set => SetValue(TrackedGameElementView.TrackedElementHeaderProperty, value);
        }

        public string Quirks
        {
            get => (string)GetValue(TrackedGameElementView.QuirksProperty);
            set => SetValue(TrackedGameElementView.QuirksProperty, value);
        }

        public bool DidWalk
        {
            get => (bool)GetValue(TrackedGameElementView.DidWalkProperty);
            set => SetValue(TrackedGameElementView.DidWalkProperty, value);
        }

        public bool DidRun
        {
            get => (bool)GetValue(TrackedGameElementView.DidRunProperty);
            set => SetValue(TrackedGameElementView.DidRunProperty, value);
        }

        public bool DidJump
        {
            get => (bool)GetValue(TrackedGameElementView.DidJumpProperty);
            set => SetValue(TrackedGameElementView.DidJumpProperty, value);
        }

        public bool IsProne
        {
            get => (bool)GetValue(TrackedGameElementView.IsProneProperty);
            set => SetValue(TrackedGameElementView.IsProneProperty, value);
        }

        public bool TrackHeat
        {
            get => (bool)GetValue(TrackedGameElementView.TrackHeatProperty);
            set => SetValue(TrackedGameElementView.TrackHeatProperty, value);
        }

        public int CurrentHeatLevel
        {
            get => (int)GetValue(TrackedGameElementView.CurrentHeatLevelProperty);
            set => SetValue(TrackedGameElementView.CurrentHeatLevelProperty, value);
        }

        public int CurrentHeatSinks
        {
            get => (int)GetValue(TrackedGameElementView.CurrentHeatSinksProperty);
            set => SetValue(TrackedGameElementView.CurrentHeatSinksProperty, value);
        }

        public int HexesMoved
        {
            get => (int)GetValue(TrackedGameElementView.HexesMovedProperty);
            set => SetValue(TrackedGameElementView.HexesMovedProperty, value);
        }

        public int NumberOfElements
        {
            get => (int)GetValue(TrackedGameElementView.NumberOfElementsProperty);
            set => SetValue(TrackedGameElementView.NumberOfElementsProperty, value);
        }

        public string PilotName
        {
            get => (string)GetValue(TrackedGameElementView.PilotNameProperty);
            set => SetValue(TrackedGameElementView.PilotNameProperty, value);
        }

        public string Notes
        {
            get => (string)GetValue(TrackedGameElementView.NotesProperty);
            set => SetValue(TrackedGameElementView.NotesProperty, value);
        }

        public int PilotSkill
        {
            get => (int)GetValue(TrackedGameElementView.PilotSkillProperty);
            set => SetValue(TrackedGameElementView.PilotSkillProperty, value);
        }

        public int PilotGunnerySkill
        {
            get => (int)GetValue(TrackedGameElementView.PilotGunnerySkillProperty);
            set => SetValue(TrackedGameElementView.PilotGunnerySkillProperty, value);
        }

        public int PilotHits
        {
            get => (int)GetValue(TrackedGameElementView.PilotHitsProperty);
            set => SetValue(TrackedGameElementView.PilotHitsProperty, value);
        }

        public string Movement
        {
            get => (string)GetValue(TrackedGameElementView.MovementProperty);
            set => SetValue(TrackedGameElementView.MovementProperty, value);
        }

        public List<string> UnitActions
        {
            get => (List<string>)GetValue(TrackedGameElementView.UnitActionsProperty);
            set => SetValue(TrackedGameElementView.UnitActionsProperty, value);
        }

        public List<string> UnitStatuses
        {
            get => (List<string>)GetValue(TrackedGameElementView.UnitStatusesProperty);
            set => SetValue(TrackedGameElementView.UnitStatusesProperty, value);
        }

        public string SelectedUnitAction
        {
            get => (string)GetValue(TrackedGameElementView.SelectedUnitActionProperty);
            set => SetValue(TrackedGameElementView.SelectedUnitActionProperty, value);
        }

        public string SelectedUnitStatus
        {
            get => (string)GetValue(TrackedGameElementView.SelectedUnitStatusProperty);
            set => SetValue(TrackedGameElementView.SelectedUnitStatusProperty, value);
        }

        public Command Heat1Command
        {
            get => (Command)GetValue(TrackedGameElementView.Heat1CommandProperty);
            set => SetValue(TrackedGameElementView.Heat1CommandProperty, value);
        }

        public Command Heat2Command
        {
            get => (Command)GetValue(TrackedGameElementView.Heat2CommandProperty);
            set => SetValue(TrackedGameElementView.Heat2CommandProperty, value);
        }

        public Command Heat5Command
        {
            get => (Command)GetValue(TrackedGameElementView.Heat5CommandProperty);
            set => SetValue(TrackedGameElementView.Heat5CommandProperty, value);
        }

        public HeatLevels HeatColorLevel
        {
            get => (HeatLevels)GetValue(TrackedGameElementView.HeatColorLevelProperty);
            set => SetValue(TrackedGameElementView.HeatColorLevelProperty, value);
        }

        public string HeatToolTip
        {
            get => (string)GetValue(TrackedGameElementView.HeatToolTipProperty);
            set => SetValue(TrackedGameElementView.HeatToolTipProperty, value);
        }

        public TrackedGameElementView()
        {
            InitializeComponent();
        }
    }
}