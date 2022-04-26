using System.Collections.Generic;
using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class ComponentFactory
    {
        public const string UNNAMED_WEAPON = "<Unnamed>";
        private const int SRM_DAMAGE_PER = 2;
        private const int LRM_DAMAGE_PER = 1;

        public static UnitComponent BuildComponentFromTemplate(UnitComponent template)
            => new UnitComponent()
            {
                Armor = template.Armor, 
                Name = template.Name, 
                RearArmor = template.RearArmor,
                Structure = template.Structure,
                OriginalArmor = template.OriginalArmor,
                OriginalRearArmor = template.OriginalRearArmor,
                OriginalStructure = template.OriginalStructure
            };

        public static Equipment BuildEquipmentFromTemplate(Equipment template)
        {
            var equipment = new Equipment()
            {
                Name = template.Name,
                Hits = template.Hits,
                Location = template.Location
            };
            equipment.CacheLocation();
            return equipment;
        }

        public static Weapon BuildWeaponFromTemplate(Weapon template)
            => BuildWeaponFromTemplate(template, overrideLocation: template.Location, keepTemplatedAmmo: true);

        public static Weapon BuildWeaponFromTemplate(Weapon template, string overrideLocation, bool keepTemplatedAmmo)
        {
            var wpn = new Weapon()
            {
                Ammo = new List<Ammunition>(template.Ammo),
                Damage = template.Damage,
                DamageCodes = new List<string>(template.DamageCodes),
                Heat = template.Heat,
                Hits = template.Hits,
                Location = overrideLocation,
                LongRange = template.LongRange,
                MediumRange = template.MediumRange,
                MinimumRange = template.MinimumRange,
                Name = template.Name,
                ShortRange = template.ShortRange,
                UsesAmmunition = template.UsesAmmunition
            };
            wpn.CacheLocation();

            if (!keepTemplatedAmmo) wpn.Ammo = new List<Ammunition>();
            return wpn;
        }

        public static Ammunition BuildAmmoFromTemplate(Ammunition template)
        {
            var ammo = new Ammunition()
            {
                AmmoCount = template.AmmoCount,
                Hits = template.Hits,
                Location = template.Location,
                Name = template.Name,
                WeaponDamage = template.WeaponDamage
            };
            ammo.CacheLocation();
            return ammo;
        }

        public static int GetAmmunitionDamagePerShotFromName(Weapon wpn)
        {
            // most weapons the explosion damage is simply the number of shots * the damage each shot does
            // however SRM and LRMs - a shot = how many missiles fire out
            if (wpn.Name.Contains("SRM 2")) return 4;
            if (wpn.Name.Contains("SRM 4")) return 8;
            if (wpn.Name.Contains("SRM 6")) return 12;
            if (wpn.Name.Contains("LRM 5")) return 5;
            if (wpn.Name.Contains("LRM 10")) return 10;
            if (wpn.Name.Contains("LRM 15")) return 15;
            if (wpn.Name.Contains("LRM 20")) return 20;

            return wpn.Damage;
        }

        /// <summary>
        /// Takes an array of a list of units and goes through all of them to pull distinct weaponry (without ammunition).
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public static IEnumerable<Weapon> GetDistinctWeapons(List<BaseUnit>[] units, string defaultLocation)
        {
            var distinctWeapons = new List<Weapon>();
            
            foreach (var unit in units)
            {
                PopulateDistinctWeaponsListWithMissingWeaponsFromUnits(distinctWeapons, unit);
            }

            distinctWeapons = distinctWeapons.OrderBy(p => p.Name).ToList();
            distinctWeapons.Insert(0, new Weapon() { Name = UNNAMED_WEAPON, Hits = 1, Location = defaultLocation });
            return distinctWeapons;
        }

        private static void PopulateDistinctWeaponsListWithMissingWeaponsFromUnits(IList<Weapon> distinctWeapons, IEnumerable<BaseUnit> units)
        {
            foreach (var unit in units)
            {
                foreach (var wpn in unit.Weapons)
                {
                    if (distinctWeapons.Any(p => p.Name == wpn.Name)) continue;
                    distinctWeapons.Add(wpn);
                }
            }
        }
    }
}