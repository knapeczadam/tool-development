using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W09_LabTest.Model
{
    //TODO: Will require some modifications
    public class Match
    {
        [JsonProperty("match_name")]
        public string Name { get; set; } = "";
        
        [JsonProperty("legend_ids")]
        public List<Guid> LegendIds { get; set; } = new List<Guid>();
        
        [JsonProperty("map_id")]
        public Guid MapId { get; set; }
    }
}
