using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W07_HearthstoneCards_Async.Model;


	public class Card
	{
		[JsonProperty("cardId")]
		public string? CardId { get; set; }
		[JsonProperty("dbfId")]
		public int DBF_ID { get; set; }
		[JsonProperty("name")]
		public string? Name { get; set; }
		[JsonProperty("cardSet")]
		public string? Set { get; set; }
		[JsonProperty("type")]
		public string? Type { get; set; }
		[JsonProperty("text")]
		public string? Text { get; set; }
		[JsonProperty("playerClass")]
		public string? PlayerClass { get; set; }
		[JsonProperty("locale")]
		public string? Locale { get; set; }

		public override string ToString()
		{
			return $"[{Type}] {Name} ({Set})";
		}
	}
