using System.Collections.Generic;
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
            typeof(MatchSettingsView));

        public static readonly BindableProperty UnitActionsProperty = BindableProperty.Create(
            nameof(UnitActions),
            typeof(List<string>),
            typeof(UnitSelectorView));

        public static readonly BindableProperty SelectedUnitActionProperty = BindableProperty.Create(
            nameof(SelectedUnitAction),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty QuirksProperty = BindableProperty.Create(
            nameof(Quirks),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty DidWalkProperty = BindableProperty.Create(
            nameof(DidWalk),
            typeof(bool),
            typeof(MatchSettingsView));

        public static readonly BindableProperty DidRunProperty = BindableProperty.Create(
            nameof(DidRun),
            typeof(bool),
            typeof(MatchSettingsView));

        public static readonly BindableProperty DidJumpProperty = BindableProperty.Create(
            nameof(DidJump),
            typeof(bool),
            typeof(MatchSettingsView));

        public static readonly BindableProperty IsProneProperty = BindableProperty.Create(
            nameof(IsProne),
            typeof(bool),
            typeof(MatchSettingsView));

        public static readonly BindableProperty CurrentHeatLevelProperty = BindableProperty.Create(
            nameof(CurrentHeatLevel),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty CurrentHeatSinksProperty = BindableProperty.Create(
            nameof(CurrentHeatSinks),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty HexesMovedProperty = BindableProperty.Create(
            nameof(HexesMoved),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty NumberOfElementsProperty = BindableProperty.Create(
            nameof(NumberOfElements),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty PilotNameProperty = BindableProperty.Create(
            nameof(PilotName),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty PilotSkillProperty = BindableProperty.Create(
            nameof(PilotSkill),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty PilotGunnerySkillProperty = BindableProperty.Create(
            nameof(PilotGunnerySkill),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty PilotHitsProperty = BindableProperty.Create(
            nameof(PilotHits),
            typeof(int),
            typeof(MatchSettingsView));

        public static readonly BindableProperty NotesProperty = BindableProperty.Create(
            nameof(Notes),
            typeof(string),
            typeof(MatchSettingsView));

        public static readonly BindableProperty MovementProperty = BindableProperty.Create(
            nameof(Movement),
            typeof(string),
            typeof(MatchSettingsView));

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

        public string SelectedUnitAction
        {
            get => (string)GetValue(TrackedGameElementView.SelectedUnitActionProperty);
            set => SetValue(TrackedGameElementView.SelectedUnitActionProperty, value);
        }

        public TrackedGameElementView()
        {
            InitializeComponent();
        }
    }
}