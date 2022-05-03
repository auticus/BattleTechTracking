using System;
using System.Globalization;
using System.Text;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class QuirksToDescriptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var quirks = value.ToString().Split(',');
            var sb = new StringBuilder();
            foreach (var quirk in quirks)
            {
                sb.AppendLine(Quirks.GetQuirkDescription(quirk.Trim()));
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
