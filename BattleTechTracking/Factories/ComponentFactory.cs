using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class ComponentFactory
    {
        public static UnitComponent BuildComponentFromTemplate(UnitComponent template)
            => new UnitComponent()
            {
                Armor = template.Armor, 
                Name = template.Name, 
                RearArmor = template.RearArmor,
                Structure = template.Structure
            };

        public static Equipment BuildEquipmentFromTemplate(Equipment template)
            => new Equipment()
            {
                Name = template.Name,
                Hits = template.Hits,
                Location = template.Location
            };

        public static Weapon BuildWeaponFromTemplate(Weapon template)
            => new Weapon()
            {
                Ammo = new List<Ammunition>(template.Ammo),
                Damage = template.Damage,
                DamageCodes = new List<string>(template.DamageCodes),
                Heat = template.Heat,
                Hits = template.Hits,
                Location = template.Location,
                LongRange = template.LongRange,
                MediumRange = template.MediumRange,
                MinimumRange = template.MinimumRange,
                Name = template.Name,
                ShortRange = template.ShortRange
            };

        public static Ammunition BuildAmmoFromTemplate(Ammunition template)
            => new Ammunition()
            {
                AmmoCount = template.AmmoCount,
                Hits = template.Hits,
                Location = template.Location,
                Name = template.Name
            };
    }
}