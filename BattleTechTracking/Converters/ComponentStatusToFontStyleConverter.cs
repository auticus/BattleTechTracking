using System;
using System.Globalization;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class ComponentStatusToFontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (UnitComponentStatus)value;

            return status == UnitComponentStatus.Destroyed ? FontAttributes.None : FontAttributes.Bold;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
