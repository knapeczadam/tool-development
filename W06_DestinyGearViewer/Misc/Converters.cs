using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace W06_DestinyGearViewer.Misc
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    internal class DestinyIconToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strVal)
            {
                var fileName = Path.GetFileName(strVal);
                var imageData = Helpers.DataProvider.GetResource(fileName);
                return Helpers.ToImageSource(imageData);
            }

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    internal class ItemIconBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolVal)
            {
                return boolVal ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.LightGray);
            }

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
