using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W06_HeroContactList.Models;


    internal class JsonHeroContactService : IHeroContactService
    {
        public List<HeroContact> LoadContacts(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var jsonData = File.ReadAllText(filePath);
            var contacts = JsonConvert.DeserializeObject<List<HeroContact>>(jsonData);

            // Load image sources
            contacts.ForEach(contact =>
            {
                contact.Avatar = Helpers.GetImageSource($"Resources/{contact.AvatarPath}");
            });

            return contacts;
        }

        public void SaveContacts(string filePath, List<HeroContact> contacts)
        {
            var jsonData = JsonConvert.SerializeObject(contacts);
            File.WriteAllText(filePath, jsonData);
        }
    }
