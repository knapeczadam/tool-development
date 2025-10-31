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

namespace W05_DataBinding.ElementToElement;


    /// <summary>
    /// Interaction logic for BindingMode.xaml
    /// </summary>
    public partial class BindingMode : UserControl
    {
        public BindingMode()
        {
            InitializeComponent();
        }

        private void btnSetSmall_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 10;
        }

        private void btnSetLarge_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 40;
        }

        private void btnSetMedium_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 25;
        }
    }
