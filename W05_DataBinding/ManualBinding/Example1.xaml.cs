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

namespace W05_DataBinding.ManualBinding;


    /// <summary>
    /// Interaction logic for Example1.xaml
    /// </summary>
    public partial class Example1 : UserControl
    {
        public Example1()
        {
            InitializeComponent();
            lblText.FontSize = sliderFontSize.Value;
        }
        private void SliderFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblText == null) return; // Check if lblText is initialized
            lblText.FontSize = sliderFontSize.Value;
        }

        private void BtnSetSmall_Click(object sender, RoutedEventArgs e)
        {
            sliderFontSize.Value = 10;
        }

        private void BtnSetMedium_Click(object sender, RoutedEventArgs e)
        {
            sliderFontSize.Value = 25;
        }

        private void BnSetLarge_Click(object sender, RoutedEventArgs e)
        {
            sliderFontSize.Value = 40;
        }
    }
