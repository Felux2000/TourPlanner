using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace TourPlanner.Converters
{
    public class DistanceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var distance = (float)value;

            var suffix = "m";
            if (distance > 1000)
            {
                suffix = "km";
                distance = (float) Math.Round(distance/1000,2);
            }
            return $"{distance} {suffix}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
