using System.Collections.Generic;

namespace BattleTechTracking.Reports
{
    public class DataReport
    {
        public const string DAMAGE_REPORT = "Damage Report";
        public const string DETERMINING_CRITICAL_HITS = "Determining Critical Hits Table";
        public const string MECH_HIT_LOCATION_TABLE = "Mech Hit Location Table";

        public static IEnumerable<string> GetDataReportList()
        {
            var reports = new List<string>
            {
                DAMAGE_REPORT,
                DETERMINING_CRITICAL_HITS,
                MECH_HIT_LOCATION_TABLE
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
                { DAMAGE_REPORT, new DamageReport() },
                { DETERMINING_CRITICAL_HITS, new DetermineCritHitsTable()},
                { MECH_HIT_LOCATION_TABLE, new MechHitLocationTable()}
            };

            return dictionary;
        }
    }
}
