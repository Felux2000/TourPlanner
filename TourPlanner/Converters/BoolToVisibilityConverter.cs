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
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility FalseValue = GetBoolValue(parameter,Inverse) ? Visibility.Visible : Visibility.Hidden;
            Visibility TrueValue = GetBoolValue(parameter, Inverse) ? Visibility.Hidden : Visibility.Visible;

            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }

        private bool GetBoolValue(object parameter, bool defaultValue)
        {
            bool i;
            if (parameter != null)
                try
                {
                    i = System.Convert.ToBoolean(parameter);
                }
                catch
                {
                    i = defaultValue;
                }
            else
                i = defaultValue;
            return i;
        }
    }
}
