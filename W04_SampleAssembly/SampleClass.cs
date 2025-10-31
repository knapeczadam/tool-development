using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04_SampleAssembly;


public class SampleClass
{
    private string _privateField = "You should not be able to alter this";

    public string PublicProperty
    {
        get => _privateField;
        set => _privateField = value;
    }

    public static string StaticMethodCall()
    {
        return $"[Called StaticMethodCall]";
    }

    public string PublicMethodCall(string param)
    {
        return $"[Called PublicMethodCall] param={param}";
    }

    public string PublicMethodCallGeneric<T>(T param)
    {
        return $"[Called PublicMethodCallGeneric] param={param}";
    }

    private string PrivateMethodCall(string param)
    {
        return $"[Called PrivateMethodCall] param={param}";
    }
}
