using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W06_DestinyGearViewer.Misc;
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.DataProviders
{
    internal class JsonDataProvider : IDestinyDataProvider
    {
        public DestinyPlayer GetMockData()
        {
            return GetPlayerData("ToMiHa");
        }

        public DestinyPlayer GetPlayerData(string playerName)
        {
            var filePath = Path.Combine("DATA", $"{playerName}.json");
            
            //Design-Time?
            if (Helpers.IsDesignMode)
            {
                filePath = Path.Combine(Helpers.ProjectDir, filePath);
            }


            if (!File.Exists(filePath))
            {
                return null;
                //throw new FileNotFoundException($"Player ({filePath}) not found");
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<DestinyPlayer>(json);
        }

        public byte[] GetResource(string resourceName)
        {
            var filePath = Path.Combine("DATA", "Resources", resourceName);

            //Design-Time?
            if (Helpers.IsDesignMode)
            {
                filePath = Path.Combine(Helpers.ProjectDir, filePath);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Resource ({filePath}) not found");
            }

            return File.ReadAllBytes(filePath);
        }
    }
}
