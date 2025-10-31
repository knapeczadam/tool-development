using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W06_DestinyGearViewer.Models
{
    public class DestinyItem
    {
        [XmlElement("itemName")]
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [XmlElement("itemDescription")]
        [JsonProperty("itemDescription")]
        public string ItemDescription { get; set; }

        [XmlElement("iconPath")]
        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [XmlElement("tierType")]
        [JsonProperty("tierType")]
        public TierTypes TierType { get; set; }

        [XmlElement("classType")]
        [JsonProperty("classType")]
        public ClassTypes ClassType { get; set; }

        [XmlElement("damageType")]
        [JsonProperty("damageType")]
        public DamageTypes DamageType { get; set; }

        [XmlElement("isGridComplete")]
        [JsonProperty("isGridComplete")]
        public bool IsGridComplete { get; set; }
    }
}
