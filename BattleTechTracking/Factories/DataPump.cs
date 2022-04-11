using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BattleTechTracking.Models;
using Newtonsoft.Json;

namespace BattleTechTracking.Factories
{
    /// <summary>
    /// The <see cref="DataPump"/> is responsible for pulling data from app data and passing it back to the application
    /// </summary>
    public class DataPump
    {
        /// <summary>
        /// Returns an IEnumerable of the type given in the persisted saved JSON files of the application.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetPersistedDataForType<T>()
        {
            //System.Environment.ApplicationData
            var fileName = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}\\{GetFileNameForType<T>()}";
            
            if (!File.Exists(fileName))
            {
                //if it does not already exist as a saved file, get the embedded version which should come with the app and return that in its stead.
                return GetEmbeddedDataForType<T>();
            }

            var stream = File.OpenRead(fileName);
            return HydrateListFromJsonStream<T>(stream);
        }

        public static void SavePersistedDataForType<T>(IEnumerable<T> data)
        {
            //System.Environment.ApplicationData
            var fileName = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}\\{GetFileNameForType<T>()}";
            
            using (var file = File.CreateText(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }
        
        /// <summary>
        /// Returns an IEnumerable of the type given in the embedded JSON files of the application.
        /// </summary>
        /// <typeparam name="T">The type of data to return.</typeparam>
        /// <returns>A list of the models requested.</returns>
        public static IEnumerable<T> GetEmbeddedDataForType<T>()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetManifestResourceForType<T>());
            return HydrateListFromJsonStream<T>(stream);
        }

        private static IEnumerable<T> HydrateListFromJsonStream<T>(Stream jsonStream)
        {
            using (var rdr = new StreamReader(jsonStream))
            {
                var json = rdr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        private static string GetManifestResourceForType<T>()
        {
            var file = GetFileNameForType<T>();
            return $"BattleTechTracking.Resources.RestoreFiles.{file}";
        }

        private static string GetFileNameForType<T>()
        {
            if (typeof(T) == typeof(BattleMech))
            {
                return "MasterMech.json";
            }

            if (typeof(T) == typeof(IndustrialUnit))
            {
                return "IndustrialMech.json";
            }

            if (typeof(T) == typeof(Infantry))
            {
                return "Infantry.json";
            }

            if (typeof(T) == typeof(CombatVehicle))
            {
                return "CombatVehicles.json";
            }

            throw new ArgumentException($"The type {typeof(T)} was not found in {nameof(GetFileNameForType)}");
        }
    }
}
