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
    /// Interaction logic for BindingOverride.xaml
    /// </summary>
    public partial class BindingOverride : UserControl
    {
        public BindingOverride()
        {
            InitializeComponent();
        }

        private void BtnSetSmall_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 10;

        }

        private void BtnSetMedium_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 25;
        }

        private void BtnSetLarge_Click(object sender, RoutedEventArgs e)
        {
            lblText.FontSize = 40;
        }
    }
