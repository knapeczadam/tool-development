using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace W10_ApexIcons
{
	class SkillToImageSourceConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string skillType)
			{
				return $"/Resources/{skillType}.png";
			}
			throw new InvalidOperationException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	class ColorToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value is System.Drawing.Color colorA)
			{
				return new SolidColorBrush(new Color() {  R = colorA.R, G = colorA.G, B = colorA.B, A = colorA.A });
			}
			if(value is Color colorB)
			{
				return new SolidColorBrush(colorB);
			}
			throw new InvalidOperationException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value is SolidColorBrush brush)
			{
				return brush.Color;
			}
			throw new InvalidOperationException();
		}
	}
}
