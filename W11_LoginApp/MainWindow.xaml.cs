using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace W11_LoginApp;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string USER = "Hello";
        private const string PASS = "World";

        private Storyboard? _showLoader, _hideLoader;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<bool> CheckCredentials(string[] creds)
        {
            //Uncomment below to simulate work
            //await Task.Delay(2000);

            return creds != null && (creds[0].Equals(USER) && creds[1].Equals(PASS));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtUser.Text;
            var password = txtPass.Password;

            if (await CheckCredentials(new[] { userName, password }))
            {
                MessageBox.Show(this, "SUCCESS!");
            }
            else
            {
                MessageBox.Show(this, "FAILED!");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ComboBoxItem;
            if (item == null) return;

            //...
            //Check App.xaml.cs ...
        }
    }
