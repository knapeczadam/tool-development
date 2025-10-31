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
    /// Interaction logic for Example2.xaml
    /// </summary>
    public partial class Example2 : UserControl
    {
        public ManualUser User { get; set; } = new ManualUser();
        public Example2()
        {
            InitializeComponent();
            User.OnFirstNameChanged += (firstName) => lblFirstName.Content = firstName;
            User.OnLastNameChanged += (lastName) => lblLastName.Content = lastName;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            User.FirstName = User.LastName = string.Empty;
        }

        private void TxtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            User.FirstName = txtFirstName.Text;
        }

        private void TxtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            User.LastName = txtLastName.Text;
        }
    }
