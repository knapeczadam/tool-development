using System.Windows;
using W04_PluginBase;

namespace MyCoolPlugin;

[MyCustomPluginInfo(Author = "Adam", Name = "Plugin_B", Description = "IJKL")]
public class PluginB : IPlugin
{
    public void Execute()
    {
        MessageBox.Show("PluginB Executed");
    }
}
