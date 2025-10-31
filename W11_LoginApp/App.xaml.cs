using System.Configuration;
using System.Data;
using System.Windows;

namespace W11_LoginApp;

    static class ThemeMethodExtensions
    {
        public static void ApplyTheme(this Application app, string themeName)
        {
            try
            {
                var theme = Application.LoadComponent(new Uri(@$"Themes/{themeName}/Theme.xaml", UriKind.Relative));

                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(theme as ResourceDictionary);
            }
            catch (Exception)
            {
                MessageBox.Show($"Failed to load Theme ({themeName})...");
            }
        }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            this.ApplyTheme("Green");
            base.OnActivated(e);
        }
    }
