using System.Reflection;
using W04_SampleAssembly;

namespace W04_ReflectionBasics;


class Program
{
    private static void Print(string msg = "") => Console.WriteLine(msg);
    
    private static void Main(string[] args)
    {
        SampleClass sample = new SampleClass();

        var sampleType = typeof(SampleClass);
        
        Print("[SampleType] Public Methods");
        sampleType.GetMethods().ToList().ForEach(info => Print(info.Name));
        Print();
        
        Print("[SampleType] Private Methods");
        sampleType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).ToList().ForEach(info => Print(info.Name));
        Print();
        
        // call public method
        MethodInfo methodInfo = sampleType.GetMethod("PublicMethodCall");
        Print("[PublicMethodCall]");
        Print($"ReturnType={methodInfo.ReturnType.Name}");
        Print($"ParameterTypes={string.Join(',', methodInfo.GetParameters().ToList().Select(p => $"{p.ParameterType.Name} {p.Name}"))}");
        
        // invoke
        var ret = (string)methodInfo.Invoke(sample, new[] { "from program" });
        Print($"Invoke Return={ret}");
        Print();
        
        // call generic method
        methodInfo = sampleType.GetMethod("PublicMethodCallGeneric");
        methodInfo = methodInfo.MakeGenericMethod(new[] { typeof(int) });
        Print("[PublicMethodCallGeneric]");
        Print($"ReturnType={methodInfo.ReturnType.Name}");
        Print($"ParameterTypes={string.Join(',', methodInfo.GetParameters().ToList().Select(p => $"{p.ParameterType.Name} {p.Name}"))}");

        // invoke
        ret = (string)methodInfo.Invoke(sample, new object[] { 1234 });
        Print($"Invoke Return={ret}");
        Print();
        
        // call static method
        Print("[StaticMethodCall]");
        ret = (string)sampleType.GetMethod("StaticMethodCall").Invoke(null, null);
        Print($"Invoke return={ret}");
        Print();
        
        // call private method
        Print("[PrivateMethodCall]");
        methodInfo = sampleType.GetMethod("PrivateMethodCall", BindingFlags.Instance | BindingFlags.NonPublic);
        ret = (string)methodInfo.Invoke(sample, new[] { "from program" });
        Print($"Invoke Return={ret}");
        Print();
        Print();
        
        // properties & fields
        // *******************
        Print("[SampleType] Public properties & fields");
        sampleType.GetProperties().ToList().ForEach(info => Print($"(property) {info.PropertyType.Name} {info.Name}"));
        sampleType.GetFields().ToList().ForEach(info => Print($"(field) {info.FieldType.Name} {info.Name}"));
        Print();
        
        Print("[SampleType] Private properties & field");
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        sampleType.GetProperties(flags).ToList().ForEach(info => Print($"(property) {info.PropertyType.Name} {info.Name}"));
        sampleType.GetFields(flags).ToList().ForEach(info => Print($"(field) {info.FieldType.Name} {info.Name}"));
        Print();
        
        // get/set property
        var propertyInfo = sampleType.GetProperty("PublicProperty");
        Print("[PublicProperty]");
        Print($"orig_value={propertyInfo.GetValue(sample)}");
        
        propertyInfo.SetValue(sample, "changed with reflection");
        Print($"new_value={sample.PublicProperty}");
        Print();
        
        // get/set field (private)
        var fieldInfo = sampleType.GetField("_privateField", flags);
        Print("[_privateField]");
        Print($"orig_value={fieldInfo.GetValue(sample)}");
        
        fieldInfo.SetValue(sample, "private changed with reflection");
        Print($"new_value={sample.PublicProperty}");
        Print();
    }
}
