using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W06_DestinyGearViewer.Models
{
    public class DestinyCharacter
    {
        [XmlElement("subClassType")]
        [JsonProperty("subClassType")]
        public ClassTypes SubClass { get; set; }

        [XmlElement("baseLevel")]
        [JsonProperty("baseLevel")]
        public int BaseLevel { get; set; }

        [XmlElement("lightLevel")]
        [JsonProperty("lightLevel")]
        public int LightLevel { get; set; }

        [XmlElement("lighLevelIconPath")]
        [JsonProperty("lighLevelIconPath")]
        public string LighLevelIconPath { get; set; }

        [XmlElement("grimoireScore")]
        [JsonProperty("grimoireScore")]
        public int GrimoireScore { get; set; }

        [XmlElement("grimoireIconPath")]
        [JsonProperty("grimoireIconPath")]
        public string GrimoireIconPath { get; set; }

        [XmlElement("emblemPath")]
        [JsonProperty("emblemPath")]
        public string EmblemPath { get; set; }

        [XmlElement("backgroundPath")]
        [JsonProperty("backgroundPath")]
        public string BackgroundPath { get; set; }

        [XmlElement("statIntellect")]
        [JsonProperty("statIntellect")]
        public DestinyStat Intellect { get; set; }

        [XmlElement("statDiscipline")]
        [JsonProperty("statDiscipline")]
        public DestinyStat Discipline { get; set; }

        [XmlElement("statStrength")]
        [JsonProperty("statStrength")]
        public DestinyStat Strength { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        [JsonProperty("items")]
        public List<DestinyItem> Items { get; set; } = new List<DestinyItem>();
    }
}
