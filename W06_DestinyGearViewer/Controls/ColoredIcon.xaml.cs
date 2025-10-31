using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace W06_DestinyGearViewer.Controls
{
    /// <summary>
    /// Interaction logic for ColoredIcon.xaml
    /// </summary>
    public partial class ColoredIcon : UserControl
    {
        public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register(
            nameof(IconSource), typeof(ImageSource), typeof(ColoredIcon), new PropertyMetadata(default(ImageSource)));

        public ImageSource IconSource
        {
            get { return (ImageSource) GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public ColoredIcon()
        {
            InitializeComponent();
        }
    }
}
