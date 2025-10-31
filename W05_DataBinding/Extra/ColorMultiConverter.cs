using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace W05_DataBinding.Extra
{
    class ColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(
                Color.FromRgb(
                    System.Convert.ToByte(values[0]),
                    System.Convert.ToByte(values[1]),
                    System.Convert.ToByte(values[2])
                ));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
