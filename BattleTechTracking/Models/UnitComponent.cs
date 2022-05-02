using System;

namespace BattleTechTracking.Models
{
    public enum UnitComponentStatus
    {
        Undamaged = 0,
        LightlyDamage = 1,
        ModeratelyDamaged = 2,
        StructuralDamage = 3,
        Destroyed = 4
    }

    public class UnitComponent : BaseModel
    {
        private int _armor;
        private int? _rearArmor;
        private int _structure;
        private int _originalArmor;
        private int? _originalRearArmor;
        private int _originalStructure;
        private bool _removed;

        public const string HEAD_CODE = "H";
        public const string CENTER_TORSO_CODE = "CT";
        public const string RIGHT_TORSO_CODE = "RT";
        public const string LEFT_TORSO_CODE = "LT";
        public const string RIGHT_ARM_CODE = "RA";
        public const string LEFT_ARM_CODE = "LA";
        public const string RIGHT_LEG_CODE = "RL";
        public const string LEFT_LEG_CODE = "LL";
        public const string TURRET_CODE = "TU";
        public const string FRONT_CODE = "F";
        public const string REAR_CODE = "R";
        public const string RIGHT_SIDE_CODE = "RS";
        public const string LEFT_SIDE_CODE = "LS";
        public const string REAR_SIDE_CODE = "R+S";
        public const string ALL_VEHICLE = "XX";

        public const string SHOULDER = "shoulder";
        public const string LOWER_ARM_ACTUATOR = "lower arm actuator";
        public const string UPPER_ARM_ACTUATOR = "upper arm actuator";
        public const string HAND_ACTUATOR = "hand actuator";
        public const string HIP = "hip";
        public const string UPPER_LEG_ACTUATOR = "upper leg actuator";
        public const string LOWER_LEG_ACTUATOR = "lower leg actuator";
        public const string FOOT_ACTUATOR = "foot actuator";

        public string Name { get; set; }

        /// <summary>
        /// Gets and sets the event that will fire when a component is set to destroyed.
        /// </summary>
        public EventHandler OnComponentDestroyed { get; set; }
        public EventHandler OnComponentRestored { get; set; }
        public EventHandler OnComponentArmorRemoved { get; set; }

        public int Armor
        {
            get => _armor;
            set
            {
                if (_armor == value) return;
                _armor = value;
                if (_armor < 0) _armor = 0;

                OnPropertyChanged(nameof(Armor));
                OnPropertyChanged(nameof(ComponentStatus));
                if (_armor == 0) OnComponentArmorRemoved?.Invoke(this, EventArgs.Empty);
            }
        }

        public int? RearArmor
        {
            get => _rearArmor;
            set
            {
                _rearArmor = value;
                if (_rearArmor < 0) _rearArmor = 0;
                OnPropertyChanged(nameof(RearArmor));
                OnPropertyChanged(nameof(ComponentStatus));
            }
        }

        public int Structure
        {
            get => _structure;
            set
            {
                if (_structure == 0 && value > 0) OnComponentRestored?.Invoke(this, EventArgs.Empty);
                _structure = value;
                if (_structure < 0) _structure = 0;
                OnPropertyChanged(nameof(Structure));
                OnPropertyChanged(nameof(ComponentStatus));
            }
        }

        public int OriginalArmor
        {
            get => _originalArmor;
            set
            {
                _originalArmor = value;
                OnPropertyChanged(nameof(OriginalArmor));
            }
        }

        public int? OriginalRearArmor
        {
            get => _originalRearArmor;
            set
            {
                _originalRearArmor = value;
                OnPropertyChanged(nameof(OriginalRearArmor));
            }
        }

        public int OriginalStructure
        {
            get => _originalStructure;
            set
            {
                _originalStructure = value;
                OnPropertyChanged(nameof(OriginalStructure));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the component was removed from the main body (ie blown off via crit).
        /// </summary>
        public bool Removed
        {
            get => _removed;
            set
            {
                if (_removed == value) return;
                _removed = value;
                OnPropertyChanged(nameof(Removed));
                OnPropertyChanged(nameof(ComponentStatus));

                if (_removed) OnComponentDestroyed?.Invoke(this, EventArgs.Empty);
                else OnComponentRestored?.Invoke(this, EventArgs.Empty);
            }
        }

        public UnitComponentStatus ComponentStatus
        {
            // the only way that the Original Armor etc are ever set is through the tracked element factory
            // that way the data edit view should still work as normal and always get an Undamaged result
            get
            {
                if (OriginalArmor == 0 && OriginalStructure == 0) return UnitComponentStatus.Undamaged;
                if (Removed) return UnitComponentStatus.Destroyed;
                if (IsUndamaged()) return UnitComponentStatus.Undamaged;
                if (IsLightlyDamaged()) return UnitComponentStatus.LightlyDamage;
                if (IsModeratelyDamaged()) return UnitComponentStatus.ModeratelyDamaged;
                if (IsHeavilyDamaged()) return UnitComponentStatus.StructuralDamage;
                if (IsDestroyed())
                {
                    OnComponentDestroyed?.Invoke(this, EventArgs.Empty);
                    return UnitComponentStatus.Destroyed;
                }
                
                throw new ArgumentException("Component Status cannot be determined by current values");
            }
        }

        public void SetOriginalValuesFromCurrentValues()
        {
            OriginalArmor = Armor;
            OriginalRearArmor = RearArmor;
            OriginalStructure = Structure;
        }

        public static string DefaultLocation(BaseUnit unit)
        {
            if (unit.GetType() == typeof(BattleMech) || unit.GetType() == typeof(IndustrialMech))
                return CENTER_TORSO_CODE;
            if (unit.GetType() == typeof(CombatVehicle))
                return FRONT_CODE;

            return CENTER_TORSO_CODE;
        }

        private bool IsUndamaged()
            => Structure == OriginalStructure && Armor == OriginalArmor && (OriginalRearArmor == null || RearArmor == OriginalRearArmor);


        private bool IsLightlyDamaged()
            => (
                    Structure == OriginalStructure &&
                    (
                        (Armor > 0 && Armor < OriginalArmor) ||
                        (OriginalRearArmor != null && RearArmor > 0 && RearArmor < OriginalRearArmor)
                    )
                );

        private bool IsModeratelyDamaged()
            => (
                Structure == OriginalStructure &&
                (
                    (OriginalRearArmor != null && RearArmor == 0) ||
                    (Armor == 0)
                )
            );

        private bool IsHeavilyDamaged()
            => Structure < OriginalStructure && Structure > 0;

        private bool IsDestroyed()
            => Structure == 0;
    }
}
