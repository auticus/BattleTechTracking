using System;
using System.Globalization;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class WeaponFiringStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (WeaponFiringStatus)value;
            switch (status)
            {
                case WeaponFiringStatus.NotFired:
                    return "Not Fired";
                case WeaponFiringStatus.WeaponFired:
                    return "Weapon Fired";
                case WeaponFiringStatus.OutOfAmmo:
                    return "No Ammunition";
                case WeaponFiringStatus.WeaponDestroyed:
                    return "Destroyed";
                default:
                    return "?";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
