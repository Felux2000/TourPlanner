using System.Globalization;
using System.Windows.Data;

namespace TourPlanner.PresentationLayer.Converters
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
                distance = (float)Math.Round(distance / 1000, 2);
            }
            return $"{distance} {suffix}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
