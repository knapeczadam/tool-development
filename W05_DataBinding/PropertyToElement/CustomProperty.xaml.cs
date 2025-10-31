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
    /// Interaction logic for CustomProperty.xaml
    /// </summary>
    public partial class CustomProperty : UserControl
    {
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register(nameof(MyUser), typeof(User), typeof(CustomProperty),
                new PropertyMetadata(default(User)));

        public User MyUser 
        {
            get => (User)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        public CustomProperty()
        {
            MyUser = new()
            {
                FirstName = "Adam",
                LastName = "Smith"
            };
            InitializeComponent();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            MyUser.FirstName = "Unknown";
            MyUser.LastName = "Unknown";
        }
    }
