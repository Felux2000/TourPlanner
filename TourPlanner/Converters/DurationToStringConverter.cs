using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlanner.Converters
{
    public class DurationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var distance = (float)value;
            TimeSpan duration = TimeSpan.FromSeconds(distance);
            var days = duration.Days;
            if (days > 0)
                return $"{days} d {duration.Subtract(TimeSpan.FromDays(days)).ToString(@"hh\:mm")} h";
            return $"{duration.Subtract(TimeSpan.FromDays(days)).ToString(@"hh\:mm")} h";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
