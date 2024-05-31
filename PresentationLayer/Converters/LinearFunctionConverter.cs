using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlanner.PresentationLayer.Converters
{
    public class LinearFunctionConverter : IValueConverter
    {
        public double A { get; set; }
        public double B { get; set; }
        public bool NotNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double a = GetDoubleValue(parameter, A);

            double x = GetDoubleValue(value, 0.0);

            double result = (a * x) + B;
            return result > 0 ? result : (NotNull?1:0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double a = GetDoubleValue(parameter, A);

            double y = GetDoubleValue(value, 0.0);

            return (y - B) / a;
        }

        private double GetDoubleValue(object parameter, double defaultValue)
        {
            double a;
            if (parameter != null)
                try
                {
                    a = System.Convert.ToDouble(parameter);
                }
                catch
                {
                    a = defaultValue;
                }
            else
                a = defaultValue;
            return a;
        }
    }
}
