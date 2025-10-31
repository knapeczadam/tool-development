using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using W09_LabTest.Model;

namespace W09_LabTest
{
    public class MapImagePathConverter : IValueConverter
    {
        private readonly string _basePath = "/Resources/Images/maps/{0}.png";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Map mapVal)
            {
                return string.Format(_basePath,mapVal.ImageName);
            }

            if (value is string strVal)
            {
                return string.Format(_basePath, strVal);
            }

            throw new InvalidOperationException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LegendImagePathConverter : IValueConverter
    {
        private readonly string _basePath = "/Resources/Images/legends/{0}.png";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Legend legendVal)
            {
                return string.Format(_basePath, legendVal.Name);
            }

            if (value is string strVal)
            {
                return string.Format(_basePath, strVal);
            }

            throw new InvalidOperationException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
