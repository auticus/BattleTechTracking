using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    /// <summary>
    /// Converts true to false and false to true.
    /// </summary>
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !((bool)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
