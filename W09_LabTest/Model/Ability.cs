using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W09_LabTest.Model
{
    class Ability
    {
		[JsonProperty("ability_type")]
		public string? AbilityType { get; set; }

        [JsonProperty("ability_name")]
        public string? AbilityName { get; set; }
    }
}
