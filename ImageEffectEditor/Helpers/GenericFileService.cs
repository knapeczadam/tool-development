using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ToolDevExam2.Helpers;

class GenericFileService
{
    public async Task SaveJsonAsync<T>(T item, string filePath)
    {
        string json = JsonConvert.SerializeObject(item, Formatting.Indented);
        await File.WriteAllTextAsync(filePath, json);
    }
    
    public async Task<T?> LoadJsonAsync<T>(string filePath)
    {
        if (!File.Exists(filePath))
            return default;

        string json = await File.ReadAllTextAsync(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    public async Task SaveXmlAsync<T>(T item, string filePath, string? root = null)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        var serializer = root != null ? new XmlSerializer(typeof(T), new XmlRootAttribute(root)) : new XmlSerializer(typeof(T));
        await Task.Run(() => serializer.Serialize(stream, item));
    }

    public async Task<T?> LoadXmlAsync<T>(string filePath, string? root = null)
    {
        if (!File.Exists(filePath))
            return default;

        await using var stream = new FileStream(filePath, FileMode.Open);
        var serializer = root != null ? new XmlSerializer(typeof(T), new XmlRootAttribute(root)) : new XmlSerializer(typeof(T));
        return await Task.Run(() => (T?)serializer.Deserialize(stream));
    }
}
