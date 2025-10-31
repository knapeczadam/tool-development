using Newtonsoft.Json;

namespace W04_Json;


public class Person
{
    [JsonProperty("first_name")]
    public string Name { get; set; }
    [JsonProperty("age")]
    public int Age { get; set; }
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
}

internal abstract class Program
{
    private static void Main(string[] args)
    {
        Person person = new Person { Name = "John", Age = 30, IsActive = true };
        string json = JsonConvert.SerializeObject(person, Formatting.Indented);
        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);

        string jsonString = @"{'first_name':'Alice','age':25, 'is_active': false}";
        Person deserializedPerson = JsonConvert.DeserializeObject<Person>(jsonString);
        Console.WriteLine($"\nDeserialized name: {deserializedPerson.Name}");
    }
}
