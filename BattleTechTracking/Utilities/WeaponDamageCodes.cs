using System.Collections.Generic;

namespace BattleTechTracking.Utilities
{
    public static class WeaponDamageCodes
    {
        private static Dictionary<string, string> damageCodes = new Dictionary<string, string>()
        {
            {"AE", "Area-Effect Weapon (p.113)"},
            {"C", "Cluster Weapon (p.l13)"},
            {"M", "Missile Weapon - uses cluster table (p.113/114)"},
            {"DE", "Direct-Fire Energy can use targeting computer when making attacks (p.113)"},
            {"DB", "Direct-Fire Ballistic can use targeting computer when making attacks (p.113)"},
            {"P", "Pulse Weapon - apply -2 to all attacks.  Can use targeting computer except for aimed shots (p.113)"},
            {"H", "Heat-Causing Weapon (p.113)"},
            {"R#", "Rapid-Fire Weapon - fires {#} shots a round - six bullet points  (p.114)"},
            {"V", "Variable Damage Weapon - damage declines over range {#}  (p.114)"},
            {"AI", "Anti-Infantry Weapon - burst-fire attack vs infantry (p.114)"},
            {"OS", "One-Shot Weapon - can be fired once per scenario (p.114)"},
            {"PB", "Point-Blank Weapon - only be used against targets in same or adjacent hex (p.114)"},
            {"E", "Electronics - ECM systems may  jam electronic equipment  (p.114)"},
            {"CE", "Counter-Electronics - these negate Electronics weapons  (p.114)"},
            {"T", "Targeting System - Targeting Computer (p.143)"},
            {"S", "Switchable Ammo - Special Munitions (p.l40)"},
            {"PE", "Performance Enhancement  (p.114)"},
            {"F", "Flak - -2 mod vs elevated altitude target in low atmosphere (p.114)"},
            {"X", "Explosive Weapon - explodes when damaged (see Gauss Rifle p.135)"}
        };
        
        //todo something to watch for - The V codes may be attached to ranges so will need to make sure they get input properly

        public static string GetDescriptionFromCode(string code)
        {
            if (TryProcessRCode(code, out var rCode))
            {
                var entry = damageCodes["R#"];
                return $"{code.Replace("#", rCode.ToString())} - {entry.Replace("{#}", rCode.ToString())}";
            }

            if (TryProcessVCode(code, out var vCodeValue))
            {
                var entry = damageCodes["V"];
                return $"{code} - {entry.Replace("{#}", vCodeValue)}";
            }

            return damageCodes.TryGetValue(code, out var description) 
                ? $"{code} - {description}" 
                : $"Weapon Code [{code}] was not found in WeaponDamageCodes";
        }

        private static bool TryProcessRCode(string code, out int? RValue)
        {
            RValue = null;
            if (string.IsNullOrEmpty(code)) return false;
            if (code.Substring(0, 1).ToUpper() != "R") return false;
            var value = code.Substring(1, code.Length - 1);
            if (!int.TryParse(value, out var tryValue)) return false;
            RValue = tryValue;
            return true;
        }

        private static bool TryProcessVCode(string code, out string VCodeValue)
        {
            // example of a v code: V25/20/10
            VCodeValue = string.Empty;
            if (string.IsNullOrEmpty(code)) return false;
            if (code.Substring(0, 1).ToUpper() != "V") return false;
            VCodeValue = code.Substring(1, code.Length - 1);
            return true;
        }
    }
}
