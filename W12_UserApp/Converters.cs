using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using W12_UserApp.Model;

namespace W12_UserApp
{
    public class AlphaToBrushConverter : IValueConverter
    {
        private static readonly Brush GreenBrush = new SolidColorBrush(Colors.Green);
        private static readonly Brush RedBrush = new SolidColorBrush(Colors.Red);

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var str = value as string;
            if (!string.IsNullOrWhiteSpace(str) && Regex.IsMatch(str, @"^[A-Za-z]+$"))
            {
                return GreenBrush;
            }

            return RedBrush;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UserToEnableConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is User user)
            {
                // return !string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName);
                return !string.IsNullOrWhiteSpace(user.FirstName);
            }
            return false;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
