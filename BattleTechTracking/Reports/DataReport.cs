using System.Collections.Generic;

namespace BattleTechTracking.Reports
{
    public class DataReport
    {
        public const string CLUSTER_HITS_TABLE = "Cluster Hits Table";
        public const string DAMAGE_REPORT = "Damage Report";
        public const string DETERMINING_CRITICAL_HITS = "Determining Critical Hits Table";
        public const string KICK_LOCATION_TABLE = "Kick Location Table";
        public const string MECH_HIT_LOCATION_TABLE = "Mech Hit Location Table";
        public const string PUNCH_LOCATION_TABLE = "Punch Location Table";

        public static IEnumerable<string> GetDataReportList()
        {
            var reports = new List<string>
            {
                CLUSTER_HITS_TABLE,
                DAMAGE_REPORT,
                DETERMINING_CRITICAL_HITS,
                KICK_LOCATION_TABLE,
                MECH_HIT_LOCATION_TABLE,
                PUNCH_LOCATION_TABLE
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
                { DAMAGE_REPORT, new DamageReport() },
                { DETERMINING_CRITICAL_HITS, new DetermineCritHitsTable()},
                { KICK_LOCATION_TABLE, new KickLocationTable()},
                { MECH_HIT_LOCATION_TABLE, new MechHitLocationTable()},
                { PUNCH_LOCATION_TABLE, new PunchLocationTable()}
            };

            return dictionary;
        }
    }
}
