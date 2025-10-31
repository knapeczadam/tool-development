using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04_PluginBase;

[AttributeUsage(AttributeTargets.Class)]
public class MyCustomPluginInfoAttribute : Attribute
{
    public string Author      { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
}
