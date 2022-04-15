using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public ObservableCollection<TrackedUnitComponent> UnitComponents { get; } = new ObservableCollection<TrackedUnitComponent>();
        public ObservableCollection<TrackedEquipment> UnitEquipment { get; } = new ObservableCollection<TrackedEquipment>();
        public ObservableCollection<TrackedWeapon> UnitWeapons { get; } = new ObservableCollection<TrackedWeapon>();
        public ObservableCollection<TrackedAmmunition> UnitAmmunition { get; } = new ObservableCollection<TrackedAmmunition>();


        public TrackedGameElement(IDisplayListView gameElement)
        {
            GameElement = gameElement;
            Quirks = string.Join(", ", GetQuirksFromElement().Select(x => x.Name).ToArray());
            CurrentHeatSinks = GetHeatSinksFromElement();
            NumberOfElements = GetNumberOfElementsFromGameElement();
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
                UnitComponents.Add(new TrackedUnitComponent(component));
            }
        }

        private void PopulateEquipment()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var equipment in element.Equipment)
            {
                UnitEquipment.Add(new TrackedEquipment(equipment));
            }
        }

        private void PopulateWeapons()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var weapon in element.Weapons)
            {
                var wpn = new TrackedWeapon(weapon);
                wpn.OnHeatGenerated += (sender, heat) =>
                {
                    CurrentHeatLevel += heat;
                };

                UnitWeapons.Add(wpn);
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
                    UnitAmmunition.Add(new TrackedAmmunition(ammo));
                }
            }
        }
    }
}
