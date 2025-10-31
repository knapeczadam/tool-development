using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W12_UserApp.Services
{
    class GenericFileService
    {
        public async Task SaveJsonAsync<T>(List<T> items, string filePath)
        {
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<T>> LoadJsonAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            string json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public async Task SaveXmlAsync<T>(List<T> items, string filePath, string? root = null)
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            var serializer = root != null ? new XmlSerializer(typeof(List<T>), new XmlRootAttribute(root)) : new XmlSerializer(typeof(List<T>));
            await Task.Run(() => serializer.Serialize(stream, items));
        }

        public async Task<List<T>> LoadXmlAsync<T>(string filePath, string? root =  null)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            await using var stream = new FileStream(filePath, FileMode.Open);
            var serializer = root != null ? new XmlSerializer(typeof(List<T>), new XmlRootAttribute(root)) : new XmlSerializer(typeof(List<T>));
            return await Task.Run(() => (List<T>)serializer.Deserialize(stream) ?? new List<T>());
        }
    }
}
