using System;
using System.Globalization;
using System.Text;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class DamageCodesToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var codes = value.ToString().Split(',');
            var sb = new StringBuilder();
            foreach (var code in codes)
            {
                sb.AppendLine(WeaponDamageCodes.GetDescriptionFromCode(code.Trim()));
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
