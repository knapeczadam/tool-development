using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W07_UsersThreading.Model;


	public class User
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("first_name")]
		public string? FirstName { get; set; }
		
		[JsonProperty("last_name")]
		public string? LastName { get; set; }
		
		[JsonProperty("email")]
		public string? Email { get; set; }
		
		[JsonProperty("ip_address")]
		public string? IPAddress { get; set; }
	}
