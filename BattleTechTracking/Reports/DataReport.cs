using System.Collections.Generic;

namespace BattleTechTracking.Reports
{
    public class DataReport
    {
        public const string CLUSTER_HITS_TABLE = "Cluster Hits Table";
        public const string COMBAT_VEHICLE_CRIT_HITS_TABLE = "Combat Vehicle Critical Hit Table";
        public const string COMBAT_VEHICLE_HIT_LOCATION_TABLE = "Combat Vehicle Hit Location Table";
        public const string DAMAGE_REPORT = "Damage Report";
        public const string DETERMINING_CRITICAL_HITS = "Determining Critical Hits Table";
        public const string FACING_AFTER_FALL_TABLE = "Facing After Fall Table";
        public const string KICK_LOCATION_TABLE = "Kick Location Table";
        public const string MECH_HIT_LOCATION_TABLE = "Mech Hit Location Table";
        public const string MOTIVE_SYSTEM_DAMAGE_TABLE = "Motive System Damage Table";
        public const string PUNCH_LOCATION_TABLE = "Punch Location Table";
        public const string VTOL_CRIT_HIT_TABLE = "VTOL Critical Hits Table";
        public const string VTOL_HIT_LOCATION_TABLE = "VTOL Hit Location Table";

        public static IEnumerable<string> GetDataReportList()
        {
            var reports = new List<string>
            {
                CLUSTER_HITS_TABLE,
                COMBAT_VEHICLE_CRIT_HITS_TABLE,
                COMBAT_VEHICLE_HIT_LOCATION_TABLE,
                DAMAGE_REPORT,
                DETERMINING_CRITICAL_HITS,
                FACING_AFTER_FALL_TABLE,
                KICK_LOCATION_TABLE,
                MECH_HIT_LOCATION_TABLE,
                MOTIVE_SYSTEM_DAMAGE_TABLE,
                PUNCH_LOCATION_TABLE,
                VTOL_CRIT_HIT_TABLE,
                VTOL_HIT_LOCATION_TABLE
            };

            return reports;
        }

        /// <summary>
        /// Builds and returns a dictionary of data report names and the associated object that goes with them.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, dynamic> GetDataReportDictionary()
        {
            var dictionary = new Dictionary<string,dynamic>
            {
                { CLUSTER_HITS_TABLE, new ClusterHitsTable()},
                { COMBAT_VEHICLE_CRIT_HITS_TABLE, new CombatVehicleCriticalHitsTable()},
                { COMBAT_VEHICLE_HIT_LOCATION_TABLE, new CombatVehicleHitLocation()},
                { DAMAGE_REPORT, new DamageReport() },
                { DETERMINING_CRITICAL_HITS, new DetermineCritHitsTable()},
                { FACING_AFTER_FALL_TABLE, new FacingAfterFallTable()},
                { KICK_LOCATION_TABLE, new KickLocationTable()},
                { MECH_HIT_LOCATION_TABLE, new MechHitLocationTable()},
                { MOTIVE_SYSTEM_DAMAGE_TABLE, new CombatVehicleMotiveDamageTable()},
                { PUNCH_LOCATION_TABLE, new PunchLocationTable()},
                { VTOL_CRIT_HIT_TABLE, new VTOLCriticalHitsTable()},
                {VTOL_HIT_LOCATION_TABLE, new VTOLHitLocationTable()}
            };

            return dictionary;
        }
    }
}
