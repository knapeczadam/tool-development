using System.Reflection;
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
using System.IO;
using W04_PluginBase;
using Path = System.IO.Path;

namespace W04_PluginLoader;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UpdatePluginDetails();
    }

    private void UpdatePluginDetails()
    {
        // Reset
        txtAuthor.Text = string.Empty;
        txtName.Text = string.Empty;
        txtDescription.Text = string.Empty;

        // Check current selected
        var selectedPlugin = listBox.SelectedItem as IPlugin;
        gridDetails.IsEnabled = selectedPlugin != null;

        if (selectedPlugin == null)
        {
            return;
        }

        var pluginType = selectedPlugin.GetType();
        var pluginInfo = pluginType.GetCustomAttribute<MyCustomPluginInfoAttribute>();

        if (pluginInfo != null)
        {
            txtAuthor.Text = pluginInfo.Author;
            txtName.Text = pluginInfo.Name;
            txtDescription.Text = pluginInfo.Description;
        }

    }

    private void BtnExecute_Click(object sender, RoutedEventArgs e)
    {
        if (listBox.SelectedItem is IPlugin plugin)
        {
            plugin.Execute();
        }
    }

    private void BtnScan_Click(object sender, RoutedEventArgs e)
    {
        // Clear listbox
        listBox.Items.Clear();

        // scan plugin directory
        var workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        var pluginDir = Path.Combine(workingDirectory, "Plugins");

        // Check if Plugin Dir exists
        if (!Directory.Exists(pluginDir))
        {
            MessageBox.Show("Plugin directory not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // search dll files directly using the pattern
        var assemblyFiles = Directory.GetFiles(pluginDir, "*.dll");

        // found any
        if (!assemblyFiles.Any())
        {
            MessageBox.Show("No plugins found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // load each assembly
        foreach (var assemblyFile in assemblyFiles)
        {
            AddPluginsFromAssembly(assemblyFile);
        }

        if (listBox.Items.Count > 0)
        {
            listBox.SelectedIndex = 0;
        }

    }

    private void AddPluginsFromAssembly(string assemblyPath)
    {
        try
        {
            // Gather IPlugins
            var dll = Assembly.LoadFrom(assemblyPath);
            var pluginTypes = dll.GetExportedTypes().Where(type => type.IsAssignableTo(typeof(IPlugin)));

            // create instances + append to lstbx
            foreach (var pluginType in pluginTypes)
            {
                if (Activator.CreateInstance(pluginType) is IPlugin instance)
                {
                    listBox.Items.Add(instance);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error loading assembly: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdatePluginDetails();
    }
}
