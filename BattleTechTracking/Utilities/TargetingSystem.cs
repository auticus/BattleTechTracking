using System.Collections.Generic;
using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    public static class TargetingSystem
    {
        public static IEnumerable<TargetedEntity> GetAllTargetedElementsModifiers(IEnumerable<ITargetable> targets, IGunnery pilot)
                => targets.Select(target => new TargetedEntity(
                    $"{target.UnitHeader} - {target.PilotName}", 
                            GetPilotGunneryScoreBase(target, pilot), 
                            target.IsProne)).ToList();
        

        private static int GetPilotGunneryScoreBase(ITargetable target, IGunnery pilot)
        {
            var baseScore = pilot.PilotGunnerySkill +
                            GetMovementModifier(target.HexesMoved) +
                            GetJumpModifier(target.DidJump) +
                            GetProneModifier(target.IsProne) + 
                            GetHeatModifier(pilot.CurrentHeatLevel);

            if (pilot.IsProne) baseScore += 2;
            if (pilot.DidJump) baseScore += 3;
            else if (pilot.DidRun) baseScore += 2;
            else if (pilot.DidWalk) baseScore += 1;

            if (pilot is IComponentTrackable pilotMachine)
            {
                if (ComponentTracker.AreSensorsDamaged(pilotMachine)) baseScore += 2;
            }
            
            return baseScore;
        }

        private static int GetMovementModifier(int hexesMoved)
        {
            if (hexesMoved < 3) return 0;
            if (hexesMoved < 5) return 1;
            if (hexesMoved < 7) return 2;
            if (hexesMoved < 10) return 3;
            if (hexesMoved < 18) return 4;
            if (hexesMoved < 25) return 5;
            return 6;
        }

        private static int GetProneModifier(bool isProne) => isProne ? 1 : 0; //note its -2 if adjacent, but +1 any other time
        private static int GetJumpModifier(bool didJump) => didJump ? 1 : 0;

        private static int GetHeatModifier(int heatLevel)
        {
            if (heatLevel < 8) return 0;
            if (heatLevel < 13) return 1;
            if (heatLevel < 17) return 2;
            if (heatLevel < 24) return 3;
            return 4;
        }
    }
}
