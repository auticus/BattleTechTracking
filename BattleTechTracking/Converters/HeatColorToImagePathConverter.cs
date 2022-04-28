using System;
using System.Globalization;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class HeatColorToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (HeatLevels)value;
            switch (color)
            {
                case HeatLevels.None:
                    return "blackLight.png";
                case HeatLevels.Green:
                    return "greenLight.png";
                case HeatLevels.Yellow:
                    return "yellowLight.png";
                case HeatLevels.Orange:
                    return "orangeLight.png";
                case HeatLevels.Red:
                    return "redLight.png";
            }

            return "blackLight.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
