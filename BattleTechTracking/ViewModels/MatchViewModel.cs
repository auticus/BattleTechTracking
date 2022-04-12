using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class MatchViewModel : BaseViewModel
    {
        private const int FACTION_1_INDEX = 0;
        private const int FACTION_2_INDEX = 1;

        private int _activeFaction;
        private bool _settingsVisible;
        private string _activeFactionName;
        private string _faction1Name;
        private string _faction2Name;
        private readonly List<IDisplayListView>[] _factionUnits = {new List<IDisplayListView>(), new List<IDisplayListView>()};
        private IDisplayListView _selectedActiveUnit;

        /// <summary>
        /// Gets or sets the active faction index.
        /// </summary>
        public int ActiveFaction
        {
            get => _activeFaction;
            set
            {
                _activeFaction = value;
                OnPropertyChanged(nameof(ActiveFaction));
                ActiveFactionUnits = new ObservableCollection<IDisplayListView>(_factionUnits[ActiveFaction]);
                ActiveFactionName = ActiveFaction == 0 ? Faction1Name : Faction2Name;
            }
        }

        public string ActiveFactionName
        {
            get => _activeFactionName;
            set
            {
                _activeFactionName = value;
                OnPropertyChanged(nameof(ActiveFactionName));
            }
        }

        public string Faction1Name
        {
            get => _faction1Name;
            set
            {
                _faction1Name = value;
                OnPropertyChanged(nameof(Faction1Name));
            }
        }

        public string Faction2Name
        {
            get => _faction2Name;
            set
            {
                _faction2Name = value;
                OnPropertyChanged(nameof(Faction2Name));
            }
        }

        public ObservableCollection<IDisplayListView> ActiveFactionUnits { get; private set; }

        public IDisplayListView SelectedActiveUnit
        {
            get => _selectedActiveUnit;
            set
            {
                _selectedActiveUnit = value;
                OnPropertyChanged(nameof(SelectedActiveUnit));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the Settings panel is visible.
        /// </summary>
        public bool SettingsVisible
        {
            get => _settingsVisible;
            set
            {
                _settingsVisible = value;
                OnPropertyChanged(nameof(SettingsVisible));
            }
        }

        public ICommand SettingsCommand { get; }
        public ICommand SettingsOkCommand { get; }
        public ICommand OkCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ActivateFaction1Command { get; }
        public ICommand ActivateFaction2Command { get; }
        public ICommand AddUnits { get; }

        public MatchViewModel()
        {
            Faction1Name = "Faction 1";
            Faction2Name = "Faction 2";
            ActiveFaction = 0;

            SettingsCommand = new Command(() =>
            {

            });

            OkCommand = new Command(() =>
            {
                //todo: save code goes here
                PageNavigation.PopAsync();
            });

            CloseCommand = new Command(() =>
            {
                PageNavigation.PopAsync();
            });

            ActivateFaction1Command = new Command(() =>
            {
                ActiveFaction = FACTION_1_INDEX;
            });

            ActivateFaction2Command = new Command(() =>
            {
                ActiveFaction = FACTION_2_INDEX;
            });

            AddUnits = new Command(() =>
            {
                //todo: view to select a unit and add it here
            });

            SettingsCommand = new Command(() =>
            {
                SettingsVisible = true;
            });

            SettingsOkCommand = new Command(() =>
            {
                SettingsVisible = false;
            });
        }
    }
}
