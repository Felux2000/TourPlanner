﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TourPlanner.PresentationLayer.Converters
{
    public class SelectedToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility NullValue = GetBoolValue(parameter, Inverse) ? Visibility.Visible : Visibility.Hidden;
            Visibility SelectedValue = GetBoolValue(parameter, Inverse) ? Visibility.Hidden : Visibility.Visible;
            return value == null ? NullValue : SelectedValue;
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
