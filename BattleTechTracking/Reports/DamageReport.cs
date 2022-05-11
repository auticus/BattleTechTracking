using System.Collections.Generic;
using System.Text;
using BattleTechTracking.Converters;
using BattleTechTracking.Models;

namespace BattleTechTracking.Reports
{
    public class DamageReport : IDataReport
    {
        private const int FACTION_ONE_INDEX = 0;
        private const int FACTION_TWO_INDEX = 1;
        private static readonly LocationCodeToStringConverter _locCodeConverter = new LocationCodeToStringConverter();

        public string GenerateReport(TextReportInput input)
        {
            var sb = new StringBuilder();
            
            GetFactionDamageReport(sb, input, FACTION_ONE_INDEX);
            GetFactionDamageReport(sb, input, FACTION_TWO_INDEX);

            return sb.ToString();
        }

        private static void GetFactionDamageReport(StringBuilder sb, TextReportInput input, int factionIndex)
        {
            GetFactionHeader(sb, input, factionIndex);
            foreach (var element in input.FactionUnitData[factionIndex])
            {
                GetUnitDamageReport(sb, element);
            }

            sb.AppendLine("!!END OF FACTION DAMAGE REPORT!!");
        }

        private static void GetFactionHeader(StringBuilder sb, TextReportInput input, int factionIndex)
        {
            sb.AppendLine($"Faction:  {input.FactionNames[factionIndex]}\r\n");
        }

        private static void GetUnitDamageReport(StringBuilder sb, IReportable element)
        {
            sb.AppendLine($"{element.UnitHeader}");
            sb.AppendLine("====================");
            var componentDamageTaken = ProcessComponentDamageAndReturnDamageTaken(sb, element.UnitComponents);
            var equipmentDamageTaken = ProcessEquipmentAndReturnDamageTaken(sb, element.UnitEquipment);
            var weaponDamageTaken = ProcessEquipmentAndReturnDamageTaken(sb, element.UnitWeapons);
            var ammoDamageTaken = ProcessEquipmentAndReturnDamageTaken(sb, element.UnitAmmunition);

            if (!componentDamageTaken && !equipmentDamageTaken && !weaponDamageTaken && !ammoDamageTaken)
            {
                sb.AppendLine("No Damage Taken");
            }

            sb.AppendLine(string.Empty);
        }

        private static int GetComponentArmorDamage(UnitComponent component) =>
            component.OriginalArmor - component.Armor;

        private static int GetComponentRearArmorDamage(UnitComponent component)
        {
            if (component.OriginalRearArmor == null) return 0;
            return component.OriginalRearArmor.Value -
                   (component.RearArmor ?? component.OriginalRearArmor.Value);
        }

        private static int GetComponentStructuralDamage(UnitComponent component) =>
            component.OriginalStructure - component.Structure;

        private static int GetEquipmentDamage(Equipment equipment) =>
            equipment.OriginalHits - equipment.Hits;

        private static bool ProcessComponentDamageAndReturnDamageTaken(StringBuilder sb, IEnumerable<UnitComponent> components)
        {
            var damageTaken = false;

            foreach (var component in components)
            {
                var armorDamage = GetComponentArmorDamage(component);
                var rearDamage = GetComponentRearArmorDamage(component);
                var structDamage = GetComponentStructuralDamage(component);

                if (armorDamage == 0 && rearDamage == 0 && structDamage == 0 && component.Removed == false) continue;
                damageTaken = true;
                sb.Append($"{component.Name}:");
                var previousElement = false;
                if (armorDamage > 0)
                {
                    damageTaken = true;
                    sb.Append($" Armor ({armorDamage})");
                    previousElement = true;
                }

                if (rearDamage > 0)
                {
                    damageTaken = true;
                    if (previousElement) sb.Append(" ::");
                    sb.Append($" Rear Armor ({rearDamage})");
                    previousElement = true;
                }

                if (structDamage > 0)
                {
                    damageTaken = true;
                    if (previousElement) sb.Append(" ::");
                    sb.Append($" Structural ({structDamage})");
                    previousElement = true;
                }

                if (component.Removed && component.Structure > 0)
                {
                    damageTaken = true;
                    if (previousElement) sb.Append(" ::");
                    sb.Append($" COMPONENT REMOVED");
                    previousElement = true;
                }

                if (component.ComponentStatus == UnitComponentStatus.Destroyed && component.Structure == 0)
                {
                    // having limbs blown off will set status to Destroyed, but the limb is not really destroyed unless structure is set to 0
                    if (previousElement) sb.Append(" ::");
                    sb.Append($" COMPONENT DESTROYED");
                }

                sb.AppendLine(string.Empty);
            }

            return damageTaken;
        }

        private static bool ProcessEquipmentAndReturnDamageTaken(StringBuilder sb, IEnumerable<Equipment> equipment)
        {
            var damageTaken = false;
            foreach (var item in equipment)
            {
                var damage = GetEquipmentDamage(item);
                if (damage == 0) continue;

                damageTaken = true;
                if (item.Location == EquipmentStatus.DESTROYED)
                {
                    sb.AppendLine($"{item.Name}: Damage = ({damage}) : COMPONENT DESTROYED");
                }
                else
                {
                    sb.AppendLine($"{item.Name}: Damage = ({damage}) : Location = {_locCodeConverter.Convert(item.Location, typeof(string), null, null)}");
                }
            }

            return damageTaken;
        }
    }
}
