using System.Collections.Generic;
using System.Linq;

namespace BattleTechTracking.Utilities
{
    public static class Quirks
    {
        public static Dictionary<string, string> quirks = new Dictionary<string, string>()
        {
            {"Accurate Weapon", "A weapon or bay is more accurate than normal and receives a -1 Modifier"},
            {"Anti-Aircraft Targeting", "-2 to target airborne units (not jumping)"},
            {"Atmospheric Flyer", "-1 to Control rolls in atmosphere"},
            {"Barrel Fist", "Does not apply +1 to punch if lacking a hand actuator"},
            {"Battle Computer", "+2 to initiative for their side, not stackable with anything else"},
            {"Battlefists", "-1 modifier to punching attacks"},
            {"Combat Computer", "Generates 4 points of heat less than normal (never less than zero)"},
            {"Command Mech", "+1 to all initiative rolls.  Not stackable with Battle Computer"},
            {"Compact Mech", "Can share a battlemech cubicle with another Compact Mech"},
            {"Cowl", "Provides bonus 3 armor to head except from attacks to the direct front"},
            {"Directional Torso Mount", "Torso can be set in any direction and it stays there until changed again"},
            {"Distracting", "+1 to Morale Checks and +1 to Demoralizer WIL Score (p179-180 TO:AR and p.74 CO"},
            {"Docking Arms", "-1 for docking attempts"},
            {"Easy to Maintain", "-1 to find replacement parts"},
            {"Easy to Pilot", "Pilot scores of 4+ receive -1 mod for pilot skill rolls from damage or terrain"},
            {"Extended Torso Twist", "Can twist 1-2 hexes instead of just one"},
            {"Fast Reload", "Reload in half the normal time"},
            {"Fine Manipulators", "Weight-free manipulators for detailed work"},
            {"Good Reputation", "Worth 10% more than normal"},
            {"Hyper-Extending Actuators", "Even if it has lower arm and/or hands it can flip its arms backward to rear arc"},
            {"Improved Communications", "Ignores first level of ghost targets (p.100 TO:AR) used against it and can always attempt Satelite uplink without giving up actions"},
            {"Improved Cooling Jacket", "The weapon generates one point less heat (never less than 1)"},
            {"Improved Life Support", "Treat units heat level 5 points lower when determining damage to pilot due to heat"},
            {"Improved Sensors", "Treated as active probe (range 4 IS; range 5 CLAN), if already equipped with one add 2 to its range"},
            {"Improved Targeting", "-1 modifier when targeting to the range given"},
            {"Internal Bomb Bay", "Unit can contain bombs (p.227 CO)"},
            {"Jettison-Capable Weapon", "The weapon can be jettisoned in combat and if recovered, remounted"},
            {"Modular Weapons", "Weapons can be replaced in half the normal time"},
            {"Multi-Trac", "Mech can track multiple targets and attack any number of targets in its front and arm firing arcs without secondary target modifier"},
            {"Narrow/Low Profile", "If margin of success to hit is 0 or 1, the hit is a glancing blow instead (half damage round down and Cluster hits apply -4 to chart)"},
            {"Nimble Jumper", "Mech can deviate one square in any direction of its jump (improving the jump range if desired)"},
            {"Overhead Arms", "As long as its standing and has line of sight, treat the arm-mounted weapons as one level higher for terrain modifiers"},
            {"Power Reverse", "Can employ running/flanking speed in reverse, but pilot skills are at +1 due to awkward position of pilot"},
            {"Protected Actuators", "Resistant to leg and swarm attacks.  The attacks receive a +1 penalty"},
            {"Reinforced Legs", "Suffer half damage to legs (round up) after performing a successful Death from Above attack"},
            {"Rugged", "Reduces frequency of needing maintenance"},
            {"Rumble Seat", "Contains extra seat in its command area"},
            {"Scout Bike", "Vehicles with this may enter light wood hexes"},
            {"Searchlight", "Equipped with a high-powered search light"},
            {"Stabilized Weapon", "If unit moves at running or flanking speed, all target numbers receive a -1 modifier"},
            {"Stable", "-1 modifier when forced to make a Piloting Skill as a result of a physical attack"},
            {"Ubiquitous", "The parts for this mech are easy to come by, receiving a -2 modifier to find replacement parts"},
            {"Variable Range Targeting", "weapon attacks at the designated range receive -1 target modifier, but attacks at the other range receive +1 mod (medium unaffected)"},
            {"Vestigial Hands", "May lift and drop items"},
            {"VTOL Rotor Arrangement", "All turn modes increased by 1 and -1 target modifier to all pilot rolls"},
            {"Ammunition Feed Problem", "On unmod attack roll of 2, roll 2d6 and on 10+ the weapon jams and cant fire again.  On a 12 the ammo explodes.  Gauss weapons explode"},
            {"Atmospheric Flight Instability", "+1 to Control rolls while in the atmosphere"},
            {"Bad Reputation", "Mech is only worth half the normal value when selling it (players must buy it at full price however)"},
            {"Cooling System Flaws", "Whenever executes or receives physical attack, falls, forced to make pilot roll because of 20+ damage, roll 2d6 and on 10+ generates 5 more heat for rest of battle"},
            {"Cramped Cockpit", "+1 piloting roll modifiers"},
            {"Difficult Ejection", "if failing pilot skill roll for ejecting, receive an additional point of damage"},
            {"Difficult to Maintain", "+1 target repair number modifier"},
            {"EM Interference", "Any energy weapons fired stop a host of electronic gear from functioning next turn (refer to p.231 CO)"},
            {"Exposed Actuators", "-1 modifier to swarm attacks and leg attacks from infantry"},
            {"Exposed Weapon Linkage", "When a location with this weapon is hit roll 2d6 and on 10+ the weapon receives crit hit"},
            {"Fragile Fuel Tank", "Any crit hit to fuel tank is an explosion on a 2d6 of 8+ instead of the normal 10.  Vehicles must roll 10+ on ANY critical hit"},
            {"Gas Hog", "When moving above cruising / safe speed consumes twice the normal rate of fuel"},
            {"Hard to Pilot", "+1 to pilot / driving skill rolls"},
            {"Illegal Design", "Harder to repair and are Obsolete (refer to p.233 CO)"},
            {"Inaccurate Weapon", "+1 modifier to target"},
            {"Large Dropship", "Takes up two docking collars instead of one"},
            {"Low-Mounted Arms", "Weapons fired from the arms count as firing from the legs which may be blocked from sight"},
            {"No/Minimal Arms", "+2 to stand up and no physical attacks with its arms"},
            {"No Cooling Jacket", "Weapon generates 2 extra heat when firing"},
            {"No Ejection System", "The mechwarrior or pilot can never eject"},
            {"No Torso Twist", "Cannot torso twist"},
            {"Non-Functional Item", "One or more components or pieces of equipment do not work"},
            {"Non-Standard Parts", "+2 penalty to finding parts"},
            {"Obsolete", "Difficult to maintain and find parts for"},
            {"Oversized", "+1 pilot roll to avoid damage passing through buildings, never receives partial cover bonus"},
            {"Poor Cooling Jacket", "Weapon generates an extra point of heat when firing"},
            {"Poor Life Support", "When determining damage to mechwarrior from heat, treat unit heat level as 5 points higher"},
            {"Poor Performance", "Cannot jump to max speed immediately.  It must spend one turn expending MP equal to walking/cruising before it can use MP to running/flanking"},
            {"Poor Sealing", "+2 when making hull breach check"},
            {"Poor Targeting, Short", "+1 to hit at short range"},
            {"Poor Targeting, Medium", "+1 to hit at medium range"},
            {"Poor Targeting, Long", "+1 to hit at long range"},
            {"Poor Workmanship", "+1 to see if critical hits occur"},
            {"Prototype", "+2 to see if critical hits occur"},
            {"Ramshackle", "Roll d6 at start of game to see what negative quirk occurs for the game (p.235 CO)"},
            {"Sensor Ghosts", "All ranged attack numbers receive a +1 modifier"},
            {"Slow Traverse", "Any turret or torso may only rotate one hexside in a turn"},
            {"Static Ammo Feed", "All ammo must be the same type throughout play"},
            {"Unbalanced", "+1 to pilot whenever mech enters a hex that requires a pilot roll"},
            {"Un-streamlined", "Cannot operate or enter the atmosphere"},
            {"Weak Head Armor", "Has less head armor than normal"},
            {"Weak Legs", "Whenever kicked or executing a Death from Above attack roll for a possible crit on EACH leg and apply the results"},
            {"Weak Undercarriage", "When landing if the pilot skill failure margin is 3 or more the landing gear(s) collapse and unit takes 50 points of damage to the nose"},
        };

        public static string GetQuirkDescription(string quirk)
        {
            // a lot of quirks have extra parameters or data, so we have to do pattern matching

            if (quirk.ToUpper() == "NONE") return "No Quirks";

            foreach (var kvp in quirks.Where(kvp => DoesThisExistInAnyForm(quirk, kvp.Key)))
            {
                return $"{quirk} :: {quirks[kvp.Key]}";
            }

            return $"{quirk} - not found in system";
        }

        private static bool DoesThisExistInAnyForm(string quirk, string contains) => quirk.ToUpper().Contains(contains.ToUpper());
    }
}
