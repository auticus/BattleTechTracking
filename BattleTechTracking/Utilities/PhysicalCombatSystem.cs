using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BattleTechTracking.Converters;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    public static class PhysicalCombatSystem
    {
        private const int KICKING_MODIFIER = -2;
        private const double PUNCHING_DAMAGE_DIVISOR = 10.0;
        private const double KICKING_DAMAGE_DIVISOR = 5.0;
        /// <summary>
        /// Gets a collection of punching components and their modifiers.
        /// </summary>
        /// <param name="element">The element being evaluated for punching modifiers.</param>
        /// <returns></returns>
        public static IEnumerable<ComponentCombatModifier> GetPunchingModifiersForCombatant(IPhysicalCombatant element)
        {
            if (!IsValidForPhysicalCombat(element.GameElement))
            {
                return new List<ComponentCombatModifier>()
                    { new ComponentCombatModifier("This element cannot punch.", "", 0) };
            }

            var components = new[]
            {
                UnitComponent.SHOULDER,
                UnitComponent.LOWER_ARM_ACTUATOR, 
                UnitComponent.UPPER_ARM_ACTUATOR,
                UnitComponent.HAND_ACTUATOR
            };

            var armComponents = element.UnitEquipment.Where(equipment => DoesElementContainDataForAnyOfTheFollowing(equipment, components)).ToList();
            var distinctLocations = armComponents.Select(arm => arm.OriginalLocation).Distinct().ToList();

            return distinctLocations.Select(loc => 
                GetPhysicalCombatStatsForArm(element, armComponents.Where(arm => arm.OriginalLocation == loc))).ToList();
        }

        /// <summary>
        /// Gets a collection of kicking components and their modifiers.
        /// </summary>
        /// <param name="element">The element being evaluated for kicking modifiers.</param>
        /// <returns></returns>
        public static IEnumerable<ComponentCombatModifier> GetKickingModifiersForCombatant(IPhysicalCombatant element)
        {
            if (!IsValidForPhysicalCombat(element.GameElement))
            {
                return new List<ComponentCombatModifier>()
                    { new ComponentCombatModifier("This element cannot kick.", "", 0) };
            }

            var components = new[]
            {
                UnitComponent.HIP,
                UnitComponent.LOWER_LEG_ACTUATOR,
                UnitComponent.UPPER_LEG_ACTUATOR,
                UnitComponent.FOOT_ACTUATOR
            };

            var legComponents = element.UnitEquipment.Where(equipment => DoesElementContainDataForAnyOfTheFollowing(equipment, components)).ToList();
            var distinctLocations = legComponents.Select(arm => arm.OriginalLocation).Distinct().ToList();

            return distinctLocations.Select(loc =>
                GetPhysicalCombatStatsForLeg(element, legComponents.Where(arm => arm.OriginalLocation == loc))).ToList();
        }

        public static int GetPunchDamage(IPhysicalCombatant element, double divisor)
        {
            if (!(element.GameElement is IVehicleDetailView vehicle)) return 0;

            var raw = (int)Math.Floor(vehicle.Tonnage / PUNCHING_DAMAGE_DIVISOR);
            return (int)Math.Floor(raw / (double)divisor);
        }

        public static int GetKickDamage(IPhysicalCombatant element, double divisor)
        {
            if (!(element.GameElement is IVehicleDetailView vehicle)) return 0;

            var raw = (int)Math.Floor(vehicle.Tonnage / KICKING_DAMAGE_DIVISOR);
            return (int)Math.Floor(raw / (double)divisor);
        }

        private static bool IsValidForPhysicalCombat(IDisplayListView element) => element is BattleMech;
        
        /// <summary>
        /// Given a set of arm components that are assumed to be from the same arm, return physical combat modifier string.
        /// </summary>
        /// <param name="pilotSkill">The piloting skill of the pilot.</param>
        /// <param name="armComponents">All equipment components that make up the arm.</param>
        /// <returns></returns>
        private static ComponentCombatModifier GetPhysicalCombatStatsForArm(IPhysicalCombatant pilot, IEnumerable<Equipment> armComponents)
        {
            var converter = new LocationCodeToStringConverter();

            //null values indicate the component is not there
            if (armComponents.First().Location == EquipmentStatus.DESTROYED)
            {
                return new ComponentCombatModifier(converter.Convert(armComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(),
                    "ARM DESTROYED",
                    null);
            }

            var shoulderMod = IsShoulderDamaged(armComponents);
            if (shoulderMod)
                return new ComponentCombatModifier(converter.Convert(armComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(),
                    "SHOULDER DAMAGED - no punching or weapon attacks with this arm",
                    0);

            var handMod = GetHandModifier(armComponents);  
            var lowerMod = GetLowerArmModifier(armComponents);
            var upperMod = GetUpperArmModifier(armComponents);
            var issues = new List<string>();

            var damageExponent = 0;
            
            if (handMod == null)
            {
                issues.Add("[NO HAND ACTUATOR]");
                handMod = 1;
                damageExponent++;
            }
            else if (handMod > 0)
            {
                issues.Add("HAND ACTUATOR DAMAGED");
                damageExponent++;
            }

            if (lowerMod == null)
            {
                issues.Add("[NO LOWER ARM ACTUATOR]");
                lowerMod = 2;
                damageExponent++;
            }
            else if(lowerMod > 0)
            {
                issues.Add("LOWER ARM ACTUATOR DAMAGED");
                damageExponent++;
            }

            if (upperMod == null)
            {
                issues.Add("[NO UPPER ARM ACTUATOR]");
                upperMod = 2;
                damageExponent++;
            }
            else if (upperMod > 0)
            {
                issues.Add("UPPER ARM ACTUATOR DAMAGED");
                damageExponent++;
            }

            var damageMod = Math.Pow(2, damageExponent);
            var damage = GetPunchDamage(pilot, damageMod);

            var sb = new StringBuilder();
            if (issues.Any()) sb.AppendLine(string.Join(", ", issues.Select(x => x.ToString()).ToArray()));
            sb.Append($"Damage: {damage}");

            return new ComponentCombatModifier(
                converter.Convert(armComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(), 
                sb.ToString(),
                pilot.PilotPilotingSkill + handMod.Value + lowerMod.Value + upperMod.Value);
        }

        private static ComponentCombatModifier GetPhysicalCombatStatsForLeg(IPhysicalCombatant pilot, IEnumerable<Equipment> legComponents)
        {
            var converter = new LocationCodeToStringConverter();
            if (legComponents.First().Location == EquipmentStatus.DESTROYED)
            {
                return new ComponentCombatModifier(converter.Convert(legComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(),
                    "LEG DESTROYED",
                    null);
            }

            var hipMod = IsHipDamaged(legComponents);
            if (hipMod)
                return new ComponentCombatModifier(converter.Convert(legComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(),
                    "HIP DAMAGED - no kicking attacks with this leg",
                    0);

            //null values indicate the component is not there
            var footMod = GetFootModifier(legComponents);
            var lowerMod = GetLowerLegModifier(legComponents);
            var upperMod = GetUpperLegModifier(legComponents);
            var issues = new List<string>();
            var damageExponent = 0;

            if (footMod == null)
            {
                issues.Add("[NO FOOT ACTUATOR]");
                footMod = 1;
            }
            else
            {
                if (footMod > 0) issues.Add("FOOT ACTUATOR DAMAGED");
            }

            if (lowerMod == null)
            {
                issues.Add("[NO LOWER LEG ACTUATOR]");
                lowerMod = 2;
                damageExponent++;
            }
            else if (lowerMod > 0)
            {
                issues.Add("LOWER LEG ACTUATOR DAMAGED");
                damageExponent++;
            }

            if (upperMod == null)
            {
                issues.Add("[NO UPPER LEG ACTUATOR]");
                upperMod = 2;
                damageExponent++;
            }
            else if (upperMod > 0)
            {
                issues.Add("UPPER LEG ACTUATOR DAMAGED");
                damageExponent++;
            }

            var damageMod = Math.Pow(2 ,damageExponent);
            var damage = GetKickDamage(pilot, damageMod);

            var sb = new StringBuilder();
            if (issues.Any()) sb.AppendLine(string.Join(", ", issues.Select(x => x.ToString()).ToArray()));
            sb.Append($"Damage: {damage}");

            return new ComponentCombatModifier(
                converter.Convert(legComponents.First().OriginalLocation, typeof(string), null, CultureInfo.CurrentCulture).ToString(),
                sb.ToString(),
                pilot.PilotPilotingSkill + KICKING_MODIFIER + footMod.Value + lowerMod.Value + upperMod.Value);
        }

        private static bool DoesElementContainDataForAnyOfTheFollowing(Equipment equipment, string[] components)
        {
            return components.Any(component => equipment.Name.ToLower().Contains(component));
        }

        private static int? GetHandModifier(IEnumerable<Equipment> armComponents)
            => GetLimbModifier(armComponents, UnitComponent.HAND_ACTUATOR, 1);
        
        private static int? GetLowerArmModifier(IEnumerable<Equipment> armComponents)
            => GetLimbModifier(armComponents, UnitComponent.LOWER_ARM_ACTUATOR, 2);
        
        private static int? GetUpperArmModifier(IEnumerable<Equipment> armComponents)
            => GetLimbModifier(armComponents, UnitComponent.UPPER_ARM_ACTUATOR, 2);
        
        private static bool IsShoulderDamaged(IEnumerable<Equipment> armComponents)
            => IsComponentDamaged(armComponents, UnitComponent.SHOULDER);

        private static int? GetFootModifier(IEnumerable<Equipment> legComponents)
            => GetLimbModifier(legComponents, UnitComponent.FOOT_ACTUATOR, 1);

        private static int? GetUpperLegModifier(IEnumerable<Equipment> legComponents)
            => GetLimbModifier(legComponents, UnitComponent.UPPER_LEG_ACTUATOR, 2);

        private static int? GetLowerLegModifier(IEnumerable<Equipment> legComponents)
            => GetLimbModifier(legComponents, UnitComponent.LOWER_LEG_ACTUATOR, 2);

        private static bool IsHipDamaged(IEnumerable<Equipment> legComponents)
            => IsComponentDamaged(legComponents, UnitComponent.HIP);

        private static int? GetLimbModifier(IEnumerable<Equipment> limbComponents, string location, int damageModifier)
        {
            var limb = limbComponents.FirstOrDefault(a => a.Name.ToLower().Contains(location));
            if (limb == null) return null;
            if (limb.Location == EquipmentStatus.DESTROYED) return damageModifier;
            return limb.Hits < limb.OriginalHits ? damageModifier : 0;
        }

        private static bool IsComponentDamaged(IEnumerable<Equipment> limbComponents, string location)
        {
            var limb = limbComponents.FirstOrDefault(a => a.Name.ToLower().Contains(location));
            if (limb == null) return false;
            if (limb.Location == EquipmentStatus.DESTROYED) return true;
            return limb.Hits < limb.OriginalHits;
        }
    }
}
