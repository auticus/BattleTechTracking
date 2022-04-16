using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BattleTechTracking.Factories;
using Xamarin.Forms;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Represents a game element that can be tracked on the game tracker and stored in a list view.
    /// </summary>
    public class TrackedGameElement : BaseModel, IDisplayListView
    {
        private IDisplayListView _gameElement;
        private int _hexesMoved;
        private bool _didWalk;
        private bool _didRun;
        private bool _didJump;
        private bool _isProne;
        private int _currentHeatLevel;
        private int _currentHeatSinks;
        private string _quirks;
        private int _numberOfElements;
        private string _pilotName;
        private int _pilotPilotingSkill;
        private int _pilotGunnerySkill;
        private int _pilotHits;
        private string _notes;

        /// <summary>
        /// Gets or sets the Game Element represented.
        /// </summary>
        public IDisplayListView GameElement
        {
            get => _gameElement;
            set
            {
                _gameElement = value;
                OnPropertyChanged(nameof(GameElement));
            }
        }

        public string UnitHeader => GameElement.UnitHeader;
        public string UnitDetails => GameElement.UnitDetails;

        public int HexesMoved
        {
            get => _hexesMoved;
            set
            {
                _hexesMoved = value;
                OnPropertyChanged(nameof(HexesMoved));
            }
        }

        public bool DidWalk
        {
            get => _didWalk;
            set
            {
                _didWalk = value;
                OnPropertyChanged(nameof(DidWalk));
            }
        }

        public bool DidRun
        {
            get => _didRun;
            set
            {
                _didRun = value;
                OnPropertyChanged(nameof(DidRun));
            }
        }

        public bool DidJump
        {
            get => _didJump;
            set
            {
                _didJump = value;
                OnPropertyChanged(nameof(DidJump));
            }
        }

        public bool IsProne
        {
            get => _isProne;
            set
            {
                _isProne = value;
                OnPropertyChanged(nameof(IsProne));
            }
        }

        public int CurrentHeatLevel
        {
            get => _currentHeatLevel;
            set
            {
                _currentHeatLevel = value;
                OnPropertyChanged(nameof(CurrentHeatLevel));
            }
        }

        public int CurrentHeatSinks
        {
            get => _currentHeatSinks;
            set
            {
                _currentHeatSinks = value;
                OnPropertyChanged(nameof(CurrentHeatSinks));
            }
        }

        public string Quirks
        {
            get => _quirks;
            set
            {
                _quirks = value;
                OnPropertyChanged(nameof(Quirks));
            }
        }

        public int NumberOfElements
        {
            get => _numberOfElements;
            set
            {
                _numberOfElements = value;
                OnPropertyChanged(nameof(NumberOfElements));
            }
        }

        public string PilotName
        {
            get => _pilotName;
            set
            {
                _pilotName = value;
                OnPropertyChanged(nameof(PilotName));
            }
        }

        public int PilotPilotingSkill
        {
            get => _pilotPilotingSkill;
            set
            {
                _pilotPilotingSkill = value;
                OnPropertyChanged(nameof(PilotPilotingSkill));
            }
        }

        public int PilotGunnerySkill
        {
            get => _pilotGunnerySkill;
            set
            {
                _pilotGunnerySkill = value;
                OnPropertyChanged(nameof(PilotGunnerySkill));
            }
        }

        public int PilotHits
        {
            get => _pilotHits;
            set
            {
                _pilotHits = value;
                OnPropertyChanged(nameof(PilotHits));
            }
        }

        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public IEnumerable<UnitComponent> UnitComponents { get; } = new List<UnitComponent>();
        public IEnumerable<Equipment> UnitEquipment { get; } = new List<Equipment>();
        public IEnumerable<Weapon> UnitWeapons { get; } = new List<Weapon>();
        public IEnumerable<Ammunition> UnitAmmunition { get; } = new List<Ammunition>();


        public TrackedGameElement(IDisplayListView gameElement)
        {
            GameElement = gameElement;
            Quirks = string.Join(", ", GetQuirksFromElement().Select(x => x.Name).ToArray());
            CurrentHeatSinks = GetHeatSinksFromElement();
            NumberOfElements = GetNumberOfElementsFromGameElement();
            PilotName = "Unknown";
            
            switch (gameElement)
            {
                case BattleMech _:
                    PilotHits = 6;
                    break;
                case CombatVehicle _:
                    PilotHits = 1;
                    break;
            }

            PopulateComponents();
            PopulateEquipment();
            PopulateWeapons();
            PopulateAmmunition();
        }

        /// <summary>
        /// Resets an element for the beginning of a new turn.
        /// </summary>
        public void NextRound()
        {
            HexesMoved = 0;
            DidWalk = false;
            DidRun = false;
            DidJump = false;
            //prone intentionally not reset

            HandleHeat();
        }

        private void HandleHeat()
        {
            if (!(GameElement is BattleMech element))
            {
                element = GameElement as IndustrialMech;
            }

            if (element == null) return;

            CurrentHeatLevel -= element.HeatSinks;
            if (CurrentHeatLevel < 0) CurrentHeatLevel = 0;
        }

        private IEnumerable<Quirk> GetQuirksFromElement()
        {
            if (!(GameElement is BattleMech element))
            {
                element = GameElement as IndustrialMech;
            }

            return element == null ? new List<Quirk>() : new List<Quirk>(element.Quirks);
        }

        private int GetHeatSinksFromElement()
        {
            if (!(GameElement is BattleMech element))
            {
                element = GameElement as IndustrialMech;
            }

            return element == null ? 0 : element.HeatSinks;
        }

        private int GetNumberOfElementsFromGameElement()
        {
            if (!(GameElement is Infantry element))
            {
                return 1;
            }

            return element.Number;
        }

        private void PopulateComponents()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var component in element.Components)
            {
                ((List<UnitComponent>)UnitComponents).Add(ComponentFactory.BuildComponentFromTemplate(component));
            }
        }

        private void PopulateEquipment()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var equipment in element.Equipment)
            {
                ((List<Equipment>)UnitEquipment).Add(ComponentFactory.BuildEquipmentFromTemplate(equipment));
            }
        }

        private void PopulateWeapons()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var weapon in element.Weapons)
            {
                ((List<Weapon>)UnitWeapons).Add(ComponentFactory.BuildWeaponFromTemplate(weapon));
            }
        }

        private void PopulateAmmunition()
        {
            if (!(GameElement is BaseUnit element)) return;

            //infantry will not be a base unit
            foreach (var weapon in element.Weapons)
            {
                foreach (var ammo in weapon.Ammo)
                {
                    ((List<Ammunition>)UnitAmmunition).Add(ComponentFactory.BuildAmmoFromTemplate(ammo));
                }
            }
        }
    }
}
