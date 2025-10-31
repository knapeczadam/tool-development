using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W09_LabTest.Model
{
    class Legend
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonProperty("full_name")]
        public string FullName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Quote { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;

        [JsonProperty("home_world")]
        public string HomeWorld { get; set; } = string.Empty;

        public List<Ability> Abilities { get; set; } = new List<Ability>();

		public int Health { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int Speed { get; set; }
    }
}
