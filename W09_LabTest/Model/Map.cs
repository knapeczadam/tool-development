using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W09_LabTest.Model
{
    class Map
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Planet { get; set; } = string.Empty;

        public string Lore { get; set; } = string.Empty;

        public string[] Features { get; set; } = new string[0];

        [JsonProperty("image_name")]
        public string ImageName { get; set; } = string.Empty;
    }
}
