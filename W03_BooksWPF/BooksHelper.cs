using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W03_BooksWPF;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

// Book Model
public class Book
{
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("author")]
    public string Author { get; set; }
    [JsonProperty("year")]
    public int Year { get; set; }
    [JsonProperty("genre")]
    public string Genre { get; set; }
    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("is_available")]
    public bool IsAvailable { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }

    public override string ToString()
        => $"{Author} - {Title} ({Year})";
}

public static class BooksHelper
{
    public static List<Book> LoadBooks(string path = "books.xml")
    {
        if (Path.GetExtension(path).ToLower() == ".json")
        {
            return LoadBooksFromJson(path);
        }
        try
        {
            var serializer = new XmlSerializer(typeof(List<Book>), new XmlRootAttribute("Books"));
            using var reader = new StreamReader(path);
            return serializer.Deserialize(reader) as List<Book> ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading books: {e.Message}");
            return [];
        }
    }

    public static List<Book> LoadBooksFromJson(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Book>>(json);
    }

    public static void SaveBooks(List<Book> books, string path = "books.xml")
    {
        if (Path.GetExtension(path).ToLower() == ".json")
        {
            SaveBooksToJson(books, path);
            return;
        }
        try
        {
            var serializer = new XmlSerializer(typeof(List<Book>), new XmlRootAttribute("Books"));
            using var writer = new StreamWriter(path);
            serializer.Serialize(writer, books);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving books: {e.Message}");
        }
    }

    public static void SaveBooksToJson(List<Book> books, string path)
    {
        string json = JsonConvert.SerializeObject(books, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
