using System.Reflection;

namespace W04_DynamicLoading;


internal abstract class Program
{
    private static void Print(string msg = "") => Console.WriteLine(msg);
    
    private static void Main(string[] args)
    {
        // load assembly
        // ************
        Print("[SampleAssembly] Dynamic loading:");
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var assemblyPath = Path.Join(basePath, "SampleAssembly.dll");
        var sampleAssembly = Assembly.LoadFrom(assemblyPath);
        Print($"{sampleAssembly.FullName} Loaded");
        Print();
        
        // 'Exposed Types' (public types)
        Print("[SampleAssembly] Exported types");
        sampleAssembly.GetExportedTypes().ToList().ForEach(info => Print(" > " + info.Name));
        
        // 'All types'
        Print("[SampleAssembly] types");
        sampleAssembly.GetTypes().ToList().ForEach(info => Print(" > " + info.Name));
        
        // load type 'SampleClass'
        // **********************
        // public class + public method
        Print("[SampleClass] create instance & call method");
        var sampleClassType = sampleAssembly.GetType("W04_SampleAssembly.SampleClass", true);
        var methodInfo = sampleClassType.GetMethod("PublicMethodCall");
        
        // create instance (SampleClass)
        // activator for instance creation
        var sampleClassInstance = Activator.CreateInstance(sampleClassType);
        var methodReturn = methodInfo.Invoke(sampleClassInstance, new[] { "Dynamic loading" });
        Print($"return={methodReturn}");
        Print();
        
        // load type 'InternalClass'
        // ************************
        // Internal class + private method
        Print("[InternalClass] Create instance + call method");
        var internalClassType = sampleAssembly.GetType("W04_SampleAssembly.InternalClass", true);
        methodInfo = internalClassType.GetMethod("DoSomethingPrivate", BindingFlags.Instance | BindingFlags.NonPublic);
        
        // create instance (InternalClass)
        // activator for instance createion
        var internalClassInstance = Activator.CreateInstance(internalClassType);
        methodReturn = methodInfo.Invoke(internalClassInstance, null);
        Print($"return={methodReturn}");
        Print();
    }
}
