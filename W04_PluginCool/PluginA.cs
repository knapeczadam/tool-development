using System.Windows;
using W04_PluginBase;

namespace MyCoolPlugin;

[MyCustomPluginInfo(Author = "Adam", Name = "Plugin_A", Description = "WASD")]
public class PluginA : IPlugin
{
    public void Execute()
    {
        MessageBox.Show("Plugin_A Executed");
    }
}
