using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace W11_Theme;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetTheme(string themeName)
        {
            try
            { 
                var themeFile = Application.LoadComponent(new Uri($@"Themes/{themeName}/Theme.xaml", UriKind.Relative)) as ResourceDictionary;
                Resources.MergedDictionaries.Clear();
                Resources.MergedDictionaries.Add(themeFile);
            }
            catch (Exception)
            {
                MessageBox.Show($"Theme ({themeName}) not found...");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ComboBoxItem;
            if (item == null) return;

            SetTheme((string)item.Content);
        }
    }
