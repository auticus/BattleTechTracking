using System.Collections.ObjectModel;

namespace BattleTechTracking.Factories
{
    public static class UnitTypes
    {
        public const string BATTLE_MECH = "Battle Mech";
        public const string INDUSTRIAL_MECH = "Industrial Mech";
        public const string COMBAT_VEHICLE = "Combat Vehicle";
        public const string SUPPORT_VEHICLE = "Support Vehicle";
        public const string VTOL = "VTol";
        public const string INFANTRY = "Infantry";
        public const string AEROSPACE = "Aerospace";

        public static ObservableCollection<string> BuildUnitTypesCollection() 
            => new ObservableCollection<string> { BATTLE_MECH, INDUSTRIAL_MECH, COMBAT_VEHICLE, SUPPORT_VEHICLE, VTOL, INFANTRY, AEROSPACE };
    }
}