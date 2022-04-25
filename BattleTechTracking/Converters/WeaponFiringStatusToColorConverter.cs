using System;
using System.Globalization;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class WeaponFiringStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (WeaponFiringStatus)value;
            switch (status)
            {
                case WeaponFiringStatus.NotFired:
                    return Color.WhiteSmoke;
                case WeaponFiringStatus.WeaponFired:
                    return Color.Gold;
                case WeaponFiringStatus.OutOfAmmo:
                    return Color.DarkRed;
                case WeaponFiringStatus.WeaponDestroyed:
                default:
                    return Color.DimGray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
