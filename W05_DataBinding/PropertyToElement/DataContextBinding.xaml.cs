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

namespace W05_DataBinding.PropertyToElement;


    /// <summary>
    /// Interaction logic for DataContextBinding.xaml
    /// </summary>
    public partial class DataContextBinding : UserControl
    {
        public User MyUser { get; set; }
        public DataContextBinding()
        {
            InitializeComponent();
            MyUser = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            MyUser.FirstName = "Unknown";
            MyUser.LastName = "Unknown";
        }
    }
