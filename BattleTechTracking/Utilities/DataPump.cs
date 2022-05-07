using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BattleTechTracking.Models;
using Newtonsoft.Json;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// The <see cref="DataPump"/> is responsible for pulling data from app data and passing it back to the application
    /// </summary>
    public static class DataPump
    {
        private const string MATCH_STATE_FILE_EXTENSION = ".btt";

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
            var fileName = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}\\{GetFileNameForType<T>()}";
            
            using (var file = File.CreateText(fileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }

        public static void SaveMatchState(MatchState factions, string fileName)
        {
            var filePath = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}\\{fileName}{MATCH_STATE_FILE_EXTENSION}";

            try
            {
                using (var file = File.CreateText(filePath))
                {
                    var json = JsonConvert.SerializeObject(factions, Formatting.Indented, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    
                    file.Write(json);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR WRITING FILE - {e}");
            }
        }

        public static MatchState LoadMatchState(string fileName)
        {
            var filePath = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}\\{fileName}";
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var rdr = new StreamReader(stream))
                    {
                        var json = rdr.ReadToEnd();
                        return JsonConvert.DeserializeObject<MatchState>(json, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR Reading File {e}");
            }

            return null;
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

        /// <summary>
        /// Returns a list of all of the saved file names
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAllSavedGameFileNames()
        {
            var filePath = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)}";
            var di = new DirectoryInfo(filePath);
            var files = di.GetFiles($"*{MATCH_STATE_FILE_EXTENSION}");
            return files.Select(file => Path.GetFileNameWithoutExtension(file.Name));
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

            if (typeof(T) == typeof(IndustrialMech))
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
