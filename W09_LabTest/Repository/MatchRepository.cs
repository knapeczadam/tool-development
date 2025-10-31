using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W09_LabTest.Model;

namespace W09_LabTest.Repository
{
	/// <summary>
	/// This repository saves and loads the configured matches to the user's device.
	/// </summary>
	public class MatchRepository
	{
		public async Task SaveMatches(List<Match> matches, string filePath)
		{
			//TODO: make async
			Thread.Sleep(3000);
			
			string json = JsonConvert.SerializeObject(matches, Formatting.Indented);
			//TODO: Save all match data to the drive (should match the json format described on the exam assignment sheet)
			await File.WriteAllTextAsync(filePath, json);
		}

		public async Task<List<Match>> LoadMatches(string filePath)
		{
			//TODO: make async
			Thread.Sleep(3000);
			
			//TODO: load match data from the drive
			string json = await File.ReadAllTextAsync(filePath);
			return JsonConvert.DeserializeObject<List<Match>>(json) ?? new List<Match>();
		}
	}
}
