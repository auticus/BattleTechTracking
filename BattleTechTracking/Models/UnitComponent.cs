using System;
using System.Diagnostics;

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

        public string Name { get; set; }

        public int Armor
        {
            get => _armor;
            set
            {
                _armor = value;
                OnPropertyChanged(nameof(Armor));
                OnPropertyChanged(nameof(ComponentStatus));
            }
        }

        public int? RearArmor
        {
            get => _rearArmor;
            set
            {
                _rearArmor = value;
                OnPropertyChanged(nameof(RearArmor));
                OnPropertyChanged(nameof(ComponentStatus));
            }
        }

        public int Structure
        {
            get => _structure;
            set
            {
                _structure = value;
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

        public UnitComponentStatus ComponentStatus
        {
            // the only way that the Original Armor etc are ever set is through the tracked element factory
            // that way the data edit view should still work as normal and always get an Undamaged result
            get
            {
                if (OriginalArmor == 0 && OriginalStructure == 0) return UnitComponentStatus.Undamaged;
                if (IsUndamaged()) return UnitComponentStatus.Undamaged;
                if (IsLightlyDamaged()) return UnitComponentStatus.LightlyDamage;
                if (IsModeratelyDamaged()) return UnitComponentStatus.ModeratelyDamaged;
                if (IsHeavilyDamaged()) return UnitComponentStatus.StructuralDamage;
                if (IsDestroyed()) return UnitComponentStatus.Destroyed;

                throw new ArgumentException("Component Status cannot be determined by current values");
            }
        }

        public void SetOriginalValuesFromCurrentValues()
        {
            OriginalArmor = Armor;
            OriginalRearArmor = RearArmor;
            OriginalStructure = Structure;
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
