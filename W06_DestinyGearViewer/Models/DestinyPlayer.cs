using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W06_DestinyGearViewer.Models
{
    [XmlRoot("destinyPlayer")]
    public class DestinyPlayer
    {
        [XmlElement("iconPath")]
        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [XmlElement("membershipId")]
        [JsonProperty("membershipId")]
        public long MembershipId { get; set; }

        [XmlElement("displayName")]
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [XmlArray("characters")]
        [XmlArrayItem("character")]
        [JsonProperty("characters")]
        public List<DestinyCharacter> Characters { get; set; } = new List<DestinyCharacter>();
    }
}
