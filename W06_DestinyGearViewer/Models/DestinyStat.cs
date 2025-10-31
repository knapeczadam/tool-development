using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W06_DestinyGearViewer.Models
{
    public class DestinyStat
    {
        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [XmlElement("iconPath")]
        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [XmlElement("value")]
        [JsonProperty("value")]
        public int Value { get; set; }

        [XmlElement("minimum")]
        [JsonProperty("minimum")]
        public int Minimum { get; set; }

        [XmlElement("maximum")]
        [JsonProperty("maximum")]
        public int Maximum { get; set; }
    }
}
