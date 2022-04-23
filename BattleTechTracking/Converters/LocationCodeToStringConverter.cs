using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BattleTechTracking.Factories;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class LocationCodeToStringConverter : IValueConverter
    {
        private Dictionary<string, string> _locationCodes;

        public LocationCodeToStringConverter()
        {
            LoadDictionary();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.ToString() == EquipmentStatus.DESTROYED) return EquipmentStatus.DESTROYED;

            // if the user is keying in a location, it may not yet exist - so need to take into consideration locs that don't yet exist
            if (string.IsNullOrEmpty(value?.ToString())) return "?";
            _locationCodes.TryGetValue(value.ToString(), out var location);

            return string.IsNullOrEmpty(location) ? "?" : location;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "?")
            {
                return string.Empty;
            }

            foreach (var kvp in _locationCodes.Where(kvp => kvp.Value == value.ToString()))
            {
                return kvp.Key;
            }

            throw new KeyNotFoundException();
        }

        private void LoadDictionary()
        {
            _locationCodes = new Dictionary<string, string>
            {
                {"R", "Rear" },
                {"F", "Front"},
                {"RS", "Right Side"},
                {"LS", "Left Side"},
                {"TU", "Turret"},
                {"H", "Head"},
                {"CT", "Center Torso"},
                {"RT", "Right Torso"},
                {"LT", "Left Torso"},
                {"RA", "Right Arm"},
                {"LA", "Left Arm"},
                {"RL", "Right Leg"},
                {"LL", "Left Leg"},
                {"R+S", "Rear and Sides"},
                {"XX", "Entire Vehicle"}
            };
        }
    }
}
