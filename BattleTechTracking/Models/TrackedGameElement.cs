using System;
using System.Collections.Generic;
using System.Linq;
using BattleTechTracking.Converters;
using BattleTechTracking.Factories;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Represents a game element that can be tracked on the game tracker and stored in a list view.
    /// </summary>
    public class TrackedGameElement : BaseModel, IDisplayMatchedListView
    {
        private IDisplayListView _gameElement;
        private int _hexesMoved;
        private bool _didWalk;
        private bool _didRun;
        private bool _didJump;
        private bool _isProne;
        private bool _isCrippled;
        private int _currentHeatLevel;
        private int _currentHeatSinks;
        private string _quirks;
        private int _numberOfElements;
        private string _pilotName;
        private int _pilotPilotingSkill;
        private int _pilotGunnerySkill;
        private int _pilotHits;
        private string _notes;
        private string _unitAction;
        private string _unitStatus;

        private LocationCodeToStringConverter _codeToLocationConverter = new LocationCodeToStringConverter();

        public EventHandler Invalidated { get; set; }

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

        public bool IsCrippled
        {
            get => _isCrippled;
            set
            {
                _isCrippled = value;
                OnPropertyChanged(nameof(IsCrippled));
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

        public string MovementDetails
        {
            get
            {
                if (!(GameElement is BaseUnit element))
                {
                    var infantry = GameElement as Infantry;
                    return GetInfantryMovementDetails(infantry);
                }

                if (element is BattleMech)
                {
                    return GetMechMovementDetails(element);
                }

                if (element is CombatVehicle)
                {
                    return GetVehicleMovementDetails(element);
                }

                throw new ArgumentException(
                    "Unknown element type given for GameElement in TrackedGameElement::MovementDetails");
            }
        }

        public string UnitAction
        {
            get => string.IsNullOrEmpty(_unitAction) ? ActionsFactory.NO_ACTION : _unitAction;
            set
            {
                if (_unitAction == value) return;
                _unitAction = value;
                OnPropertyChanged(nameof(UnitAction));
                Invalidated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string UnitStatus
        {
            get => string.IsNullOrEmpty(_unitStatus) ? EquipmentStatus.UNDAMAGED : _unitStatus;
            set
            {
                if (_unitStatus == value) return;
                _unitStatus = value;
                OnPropertyChanged(nameof(UnitStatus));
            }
        }

        public IEnumerable<UnitComponent> UnitComponents { get; } = new List<UnitComponent>();
        public IEnumerable<Equipment> UnitEquipment { get; } = new List<Equipment>();
        public IEnumerable<Weapon> UnitWeapons { get; } = new List<Weapon>();
        public IEnumerable<Ammunition> UnitAmmunition { get; } = new List<Ammunition>();

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public TrackedGameElement()
        {

        }

        public TrackedGameElement(IDisplayListView gameElement)
        {
            GameElement = gameElement;
            Quirks = GetQuirks();
            CurrentHeatSinks = GetHeatSinksFromElement();
            NumberOfElements = GetNumberOfElementsFromGameElement();
            PilotName = "Unknown";
            UnitAction = ActionsFactory.NO_ACTION;
            
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
            UnitAction = ActionsFactory.NO_ACTION;
        }

        private void HandleHeat()
        {
            if (!(GameElement is BattleMech element))
            {
                element = GameElement as IndustrialMech;
            }

            if (element == null) return;

            //current heat level = 8
            //current heat sinks = 5
            var newHeat = CurrentHeatLevel - CurrentHeatSinks;
            CurrentHeatLevel = CurrentHeatLevel - CurrentHeatSinks;
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
                var trackedComponent = ComponentFactory.BuildComponentFromTemplate(component);
                trackedComponent.OnComponentDestroyed += OnComponentDestroyed;
                trackedComponent.OnComponentRestored += OnComponentRestored;
                trackedComponent.OnComponentArmorRemoved += OnComponentArmorRemoved;
                ((List<UnitComponent>)UnitComponents).Add(trackedComponent);
            }
        }

        private void OnComponentRestored(object sender, EventArgs e)
        {
            var locCode = GetLocationCodeFromComponent(sender);
            RestoreEquipmentInLocation(locCode);
            RestoreWeaponsInLocation(locCode);
            RestoreAmmoInLocation(locCode);
        }

        private void OnComponentArmorRemoved(object sender, EventArgs e)
        {
            //this will fire off anytime a component's armor value is set to 0
            var locCode = GetLocationCodeFromComponent(sender);
            AdjustUnitStatusForArmorDestroyed(locCode);
        }

        private void OnComponentDestroyed(object sender, EventArgs e)
        {
            // this will fire off anytime a vehicular or mech component is listed as destroyed
            // set all locations of equipment, weapons, and ammo in that specific component to be listed as destroyed
            var locCode = GetLocationCodeFromComponent(sender);
            DestroyEquipmentInLocation(locCode);
            DestroyWeaponsInLocation(locCode);
            DestroyAmmoInLocation(locCode);
            AdjustUnitStatusForDestroyed(locCode);
        }

        private string GetLocationCodeFromComponent(object location)
        {
            var destroyedLocation = ((UnitComponent)location).Name;
            return _codeToLocationConverter.ConvertBack(destroyedLocation, typeof(string), null, null).ToString();
        }

        private void DestroyEquipmentInLocation(string locCode)
        {
            foreach (var equipment in UnitEquipment.Where(equip => equip.Location == locCode)) equipment.DestroyItem();
        }

        private void DestroyWeaponsInLocation(string locCode)
        {
            foreach (var weapon in UnitWeapons.Where(wpn => wpn.Location == locCode)) weapon.DestroyItem();
        }

        private void DestroyAmmoInLocation(string locCode)
        {
            foreach (var ammo in UnitAmmunition.Where(a => a.Location == locCode)) ammo.DestroyItem();
        }

        private void RestoreEquipmentInLocation(string locCode)
        {
            foreach (var equipment in UnitEquipment.Where(equip => equip.OriginalLocation == locCode)) equipment.TryRestoreItem();
        }

        private void RestoreWeaponsInLocation(string locCode)
        {
            foreach (var weapon in UnitWeapons.Where(wpn => wpn.OriginalLocation == locCode)) weapon.TryRestoreItem();
        }

        private void RestoreAmmoInLocation(string locCode)
        {
            foreach (var ammo in UnitAmmunition.Where(a => a.OriginalLocation == locCode)) ammo.TryRestoreItem();
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

        private string GetMechMovementDetails(BaseUnit element)
            => $"Walking: {element.UnitMovement.Walking}  Running: {element.UnitMovement.Running}  Jumping: {element.UnitMovement.Jumping}";
        

        private string GetVehicleMovementDetails(BaseUnit element)
            => $"Cruising: {element.UnitMovement.Walking}  Flanking: {element.UnitMovement.Running}  Flying: {element.UnitMovement.Jumping}";

        private string GetInfantryMovementDetails(Infantry element)
            => $"Movement MP: {element.Movement}";

        private string GetQuirks()
        {
            var quirks = GetQuirksFromElement().Select(x => x.Name).ToList();
            if (!quirks.Any()) return "None";

            return string.Join(", ", quirks);
        }

        private void AdjustUnitStatusForDestroyed(string locCode)
        {
            if (GameElement is CombatVehicle) AdjustCombatvehicleForComponentDestroyed();
            if (GameElement is BattleMech) AdjustMechForComponentDestroyed(locCode);
        }

        private void AdjustCombatvehicleForComponentDestroyed()
            //Combat vehicles are considered destroyed if any of their components are destroyed
            => this.UnitStatus = EquipmentStatus.DESTROYED;
        

        private void AdjustMechForComponentDestroyed(string locCode)
        {
            // Center Torso destroyed == destroyed for all mechs
            // Left or Right Torso destroyed == crippled for all mechs
            if (locCode == UnitComponent.CENTER_TORSO_CODE)
            {
                this.UnitStatus = EquipmentStatus.DESTROYED;
                return;
            }

            if (locCode == UnitComponent.LEFT_TORSO_CODE || locCode == UnitComponent.RIGHT_TORSO_CODE)
            {
                this.UnitStatus = EquipmentStatus.CRIPPLED;
            }
        }

        private void AdjustUnitStatusForArmorDestroyed(string locCode)
        {
            // Any component armor removed for vehicle == crippled, but if its already crippled or destroyed don't bother setting
            if (!(GameElement is CombatVehicle vehicle)) return;

            if (this.UnitStatus == EquipmentStatus.CRIPPLED || this.UnitStatus == EquipmentStatus.DESTROYED) return;
            this.UnitStatus = EquipmentStatus.CRIPPLED;
        }
    }
}
