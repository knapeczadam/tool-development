using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W07_HearthstoneCards_Async.Model;

namespace W07_HearthstoneCards_Async.Repositories;


	public static class LocalCardsRepository
	{

		private const string _cardsResourcePath = "Resources.Data.HearthstoneCards.json";
		private static List<Card> _cards = null;
		private static void LoadCardsFromFile()
		{
			string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
			Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{assemblyName}.{_cardsResourcePath}");
			
			if (stream == null) return;

			using (StreamReader reader = new StreamReader(stream))
			{
				string jsonStr = reader.ReadToEnd();
				_cards = JsonConvert.DeserializeObject<List<Card>>(jsonStr) ?? new List<Card>();
			}

			stream.Dispose();


			foreach (var card in _cards)
			{
				if (card.Type == null)
				{
					card.Type = "Unknown";
				}
			}
		}

		private static bool AreCardsLoaded()
		{
			return _cards != null && _cards.Any();
		}

		/// <summary>
		/// Function to request a list of cards. Function takes at least 3 seconds to execute.
		/// </summary>
		/// <returns>a collection of cards</returns>
		public static IEnumerable<Card> GetAllCards()
		{
			Thread.Sleep(3000); // takes 3 seconds to load.....

			if (!AreCardsLoaded())
				LoadCardsFromFile();

			return _cards;
		}

		/// <summary>
		/// Function to lookup cards of a certain type. Function takes at least 3 seconds to execute.
		/// </summary>
		/// <param name="type">type of the cards to search for</param>
		/// <returns>a collection of cards with the specified type</returns>
		public static IEnumerable<Card> GetCardsOfType(string type)
		{
			Thread.Sleep(3000); // takes 3 seconds to look this up.....

			if (!AreCardsLoaded())
				LoadCardsFromFile();

			return _cards.Where(c => c.Type.Contains(type, StringComparison.InvariantCultureIgnoreCase));
		}
	}
