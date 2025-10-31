using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace W05_DataBinding.Exercise
{
    [ValueConversion(typeof(string), typeof(bool))]
    class TextToEnableConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var textValue = value as string;
            if (string.IsNullOrEmpty(textValue))
            {
                return false;
            }
            return true;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
