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
using Microsoft.Win32;

namespace W04_AssemblyViewerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnSelectAssembly_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog
        {
            DefaultExt = ".dll",
            Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*"
        };
        
        if (dlg.ShowDialog() == true)
        {
            string fileName = dlg.FileName;

            var assembly = Assembly.LoadFrom(fileName);
            var publicTypes = assembly.GetExportedTypes();
            
            treeView.Items.Clear();

            var flags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            foreach (var type in publicTypes)
            {
                TreeViewItem typeNode = new TreeViewItem { Header = type.FullName };
                
                // Methods
                var methods = type.GetMethods(flags);
                if (methods.Length > 0)
                {
                    TreeViewItem methodsNode = new TreeViewItem { Header = "Methods" };
                    foreach (var method in methods)
                    {
                        string signature =
                            $"{method.Name}({string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))}) : {method.ReturnType.Name}";
                        methodsNode.Items.Add(new TreeViewItem { Header = signature });
                    }

                    typeNode.Items.Add(methodsNode);
                }
                
                // Properties
                var properties = type.GetProperties(flags);
                if (properties.Length > 0)
                {
                    TreeViewItem propsNode = new TreeViewItem { Header = "Properties" };
                    foreach (var prop in properties)
                    {
                        string propSignature = $"{prop.Name} : {prop.PropertyType.Name}";
                        propsNode.Items.Add(new TreeViewItem { Header = propSignature });
                    }

                    typeNode.Items.Add(propsNode);
                }
                
                // Fields
                var fields = type.GetFields(flags);
                if (fields.Length > 0)
                {
                    TreeViewItem fieldsNode = new TreeViewItem { Header = "Fields" };
                    foreach (var field in fields)
                    {
                        string fieldSignature = $"{field.Name} : {field.FieldType.Name}";
                        fieldsNode.Items.Add(new TreeViewItem { Header = fieldSignature });
                    }

                    typeNode.Items.Add(fieldsNode);
                }

                treeView.Items.Add(typeNode);
            }
        }
    }
}
