using System.Windows;
using W04_PluginBase;

namespace W04_PluginOther;

[MyCustomPluginInfo(Author = "Adam", Name = "AwesomePlugin")]
public class AwesomePlugin : IPlugin
{
    public void Execute()
    {
        MessageBox.Show("AwesomePlugin Executed!");
    }
}
