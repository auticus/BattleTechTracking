using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class AmmoDamageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dmg = (int)value;
            return $"Explosion Dmg: ({dmg})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
