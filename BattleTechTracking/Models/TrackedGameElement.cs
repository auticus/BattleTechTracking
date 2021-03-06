using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using BattleTechTracking.Converters;
using BattleTechTracking.Factories;
using BattleTechTracking.Reports;
using BattleTechTracking.Utilities;
using Newtonsoft.Json;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Represents a game element that can be tracked on the game tracker and stored in a list view.
    /// </summary>
    public class TrackedGameElement : BaseModel, IDisplayMatchedListView, 
            IReportable, 
            IHeatable, 
            ITrackable, 
            ITargetable, 
            IGunnery, 
            IComponentTrackable,
            IPhysicalCombatant
    {
        private IDisplayListView _gameElement;
        private int _hexesMoved;
        private bool _didWalk;
        private bool _didRun;
        private bool _didJump;
        private bool _isProne;
        private bool _isCrippled;
        private bool _isImmobile;
        private int _currentHeatLevel;
        private int _currentHeatSinks;
        private int _numberOfElements;
        private string _pilotName;
        private int _pilotPilotingSkill;
        private int _pilotGunnerySkill;
        private int _pilotHits;
        private string _notes;
        private string _unitAction;
        private string _unitStatus;
        private bool _sensorsDamaged;
        private bool _armOrShoulderDamaged;
        private string _punchingMod;
        private string _kickingMod;

        private readonly LocationCodeToStringConverter _codeToLocationConverter = new LocationCodeToStringConverter();

        [JsonIgnore]
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

        [JsonIgnore]
        public string UnitHeader => GameElement.UnitHeader;

        [JsonIgnore]
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

        public bool IsImmobile
        {
            get => _isImmobile;
            set
            {
                _isImmobile = value;
                OnPropertyChanged(nameof(IsImmobile));
            }
        }

        public int CurrentHeatLevel
        {
            get => _currentHeatLevel;
            set
            {
                if (!ThisCanTrackHeat) value = 0;

                if (value > Heat.MAX_HEAT) value = Heat.MAX_HEAT;
                _currentHeatLevel = value;
                OnPropertyChanged(nameof(CurrentHeatLevel));
                OnPropertyChanged(nameof(CurrentHeatColor));
                OnPropertyChanged(nameof(CurrentHeatTooltip));
            }
        }

        public bool SensorsDamaged
        {
            get => _sensorsDamaged;
            set
            {
                _sensorsDamaged = value;
                OnPropertyChanged(nameof(SensorsDamaged));
            }
        }

        public bool ArmOrShoulderDamaged
        {
            get => _armOrShoulderDamaged;
            set
            {
                _armOrShoulderDamaged = value;
                OnPropertyChanged(nameof(ArmOrShoulderDamaged));
            }
        }

        public string PunchingModifier
        {
            get => _punchingMod;
            set
            {
                _punchingMod = value;
                OnPropertyChanged(nameof(PunchingModifier));
            }
        }

        public string KickingModifier
        {
            get => _kickingMod;
            set
            {
                _kickingMod = value;
                OnPropertyChanged(nameof(KickingModifier));
            }
        }

        /// <summary>
        /// Gets the value showing the current heat color.
        /// </summary>
        public HeatLevels CurrentHeatColor => ThisCanTrackHeat ? Heat.GetHeatMapColorForLevel(CurrentHeatLevel) : HeatLevels.None;

        /// <summary>
        /// Gets the value showing the current heat's tooltip.
        /// </summary>
        [JsonIgnore]
        public string CurrentHeatTooltip =>
            ThisCanTrackHeat ? Heat.GetHeatImpactTooltip(this) : string.Empty;

        /// <summary>
        /// Gets a value indicating that this game element tracks heat.
        /// </summary>
        [JsonIgnore]
        public bool ThisCanTrackHeat => Heat.ElementTracksHeat(this);
       
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
            get
            {
                if (!(GameElement is BattleMech element))
                {
                    element = GameElement as IndustrialMech;
                }

                var quirkList = element == null ? new List<Quirk>() : element.Quirks;
                if (!quirkList.Any()) return "None";
                var quirks = quirkList.Select(x => x.Name).ToList();
                return string.Join(", ", quirks);
            }
        }

        /// <summary>
        /// Gets or sets the number of models or elements within the unit.  Mainly used for Infantry regiments.
        /// </summary>
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

        [JsonIgnore]
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

        /// <summary>
        /// Gets the <see cref="Targeting"/> for this <see cref="TrackedGameElement"/>
        /// </summary>
        [JsonIgnore]
        public Targeting ValidTargets { get; } = new Targeting();

        public ObservableCollection<UnitComponent> UnitComponents { get; } = new ObservableCollection<UnitComponent>();
        public ObservableCollection<Equipment> UnitEquipment { get; } = new ObservableCollection<Equipment>();
        public ObservableCollection<Weapon> UnitWeapons { get; } = new ObservableCollection<Weapon>();
        public ObservableCollection<Ammunition> UnitAmmunition { get; } = new ObservableCollection<Ammunition>();

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public TrackedGameElement()
        {

        }

        public TrackedGameElement(IDisplayListView gameElement)
        {
            GameElement = gameElement;
            CurrentHeatSinks = GameStateTracker.GetHeatSinksFromElement(this);
            NumberOfElements = GameStateTracker.GetNumberOfElementsFromGameElement(this);
            PilotName = GameStateTracker.DEFAULT_PILOT_NAME;
            UnitAction = ActionsFactory.NO_ACTION;
            PilotHits = GameStateTracker.GetStartingHitsForPilot(this);
            
            PopulateComponents();
            PopulateEquipment();
            PopulateWeapons();
            PopulateAmmunition();
        }

        public void RefreshComponentStatus()
        {
            // the alternative to this would be creating an elaborate event system to fire off when components are damaged
            // this was the shorter more direct way.
            SensorsDamaged = ComponentTracker.AreSensorsDamaged(this);
            ArmOrShoulderDamaged = ComponentTracker.AreAnyArmsOrShouldersDamaged(this);
            PunchingModifier = PopulatePhysicalAttackProperties(PhysicalCombatSystem.GetPunchingModifiersForCombatant);
            KickingModifier = PopulatePhysicalAttackProperties(PhysicalCombatSystem.GetKickingModifiersForCombatant);
        }

        private string PopulatePhysicalAttackProperties(Func<IPhysicalCombatant, IEnumerable<ComponentCombatModifier>> modifierFunction)
        {
            var mods = modifierFunction.Invoke(this);
            var sb = new StringBuilder();

            foreach (var mod in mods)
            {
                sb.AppendLine($"Loc: {mod.Component}");
                if (!string.IsNullOrEmpty(mod.Description)) sb.AppendLine(mod.Description);
                if (mod.CombatRoll != null) sb.AppendLine($"Pilot Roll: {mod.CombatRoll}+\r\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Populates the component collection and wires up each component to event handlers that handle the component's state.
        /// </summary>
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
                ((ObservableCollection<UnitComponent>)UnitComponents).Add(trackedComponent);
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
                var equip = ComponentFactory.BuildEquipmentFromTemplate(equipment);
                equip.OnEquipmentAttemptedToBeRestored += OnEquipmentAttemptedToBeRestored;
                ((ObservableCollection<Equipment>)UnitEquipment).Add(equip);
            }
        }

        private void PopulateWeapons()
        {
            var element = GameElement as BaseUnit;
            if (element == null) return;

            //infantry will not be a base unit
            foreach (var weapon in element.Weapons)
            {
                var wpn = ComponentFactory.BuildWeaponFromTemplate(weapon);
                wpn.OnEquipmentAttemptedToBeRestored += OnEquipmentAttemptedToBeRestored;
                ((ObservableCollection<Weapon>)UnitWeapons).Add(wpn);
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
                    var templatedAmmo = ComponentFactory.BuildAmmoFromTemplate(ammo);
                    templatedAmmo.OnEquipmentAttemptedToBeRestored += OnEquipmentAttemptedToBeRestored;
                    ((ObservableCollection<Ammunition>)UnitAmmunition).Add(ammo);
                }
            }
        }

        private void OnEquipmentAttemptedToBeRestored(object sender, EventArgs e)
        {
            // user has 0 hits on an item and has put positive value back on it.  
            // if the location that they are trying to restore is not destroyed, allow this
            var converter = new LocationCodeToStringConverter();

            if (!(sender is Equipment equipment)) throw new ArgumentException("Restored Equipment is not a type that is understood");

            var loc = converter.Convert(equipment.OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString();

            var component = UnitComponents.FirstOrDefault(c => c.Name == loc);
            if (component == null) return; //this shouldn't happen but if the converter is given a value not in its dictionary that will cause a component to not be found

            if (component.ComponentStatus != UnitComponentStatus.Destroyed)
            {
                //we are not using TryRestoreItem here because the hit on the stack will still say 0 when the event bubbles up and the try will fail
                equipment.ForceRestoreItem();
            }
            else
            {
                equipment.Hits = 0;
            }
        }

        private string GetMechMovementDetails(BaseUnit element)
            => $"Walking: {element.UnitMovement.Walking}  Running: {element.UnitMovement.Running}  Jumping: {element.UnitMovement.Jumping}";
        

        private string GetVehicleMovementDetails(BaseUnit element)
            => $"Cruising: {element.UnitMovement.Walking}  Flanking: {element.UnitMovement.Running}  Flying: {element.UnitMovement.Jumping}";

        private string GetInfantryMovementDetails(Infantry element)
            => $"Movement MP: {element.Movement}";

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
