using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace W05_DataBinding.Extra
{
    [ValueConversion(typeof(bool?), typeof(Visibility))]
    class BoolToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var boolValue = value as bool?;
            if (boolValue.HasValue && boolValue.Value)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }

            return false;
        }
    }
}
