using System.Globalization;
using System.Windows.Data;

namespace TourPlanner.PresentationLayer.Converters
{
    public class DurationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan duration = value.GetType() == typeof(TimeSpan) ? (TimeSpan)value : TimeSpan.FromSeconds((float)value);
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
