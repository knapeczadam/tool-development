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

namespace W05_DataBinding.Exercise
{
    /// <summary>
    /// Interaction logic for Exercise.xaml
    /// </summary>
    public partial class Exercise : UserControl
    {
        public Exercise()
        {
            InitializeComponent();
        }


        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            sliderG.Value = 0;
            sliderR.Value = 0;
            sliderB.Value = 0;
        }
    }
}
