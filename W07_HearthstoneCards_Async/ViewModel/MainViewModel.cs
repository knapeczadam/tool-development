using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W07_HearthstoneCards_Async.Model;
using W07_HearthstoneCards_Async.Repositories;

namespace W07_HearthstoneCards_Async.ViewModel;


	public partial class MainViewModel_Threading : ObservableObject
	{
		[ObservableProperty]
		private List<Card>? _cardsList = null;

		[ObservableProperty]
		private string _loadingText = string.Empty;

		[RelayCommand]
		private void LoadAllCards()
		{
			//loading........
			LoadingText = "(loading data.....)";

			var cardsCollection = LocalCardsRepository.GetAllCards();
			CardsList = cardsCollection.ToList();

			LoadingText = string.Empty;


		}


		[RelayCommand]
		private void LoadFilteredCards(string? searchTypes)
		{
			//do not respond if input is empty
			if (string.IsNullOrWhiteSpace(searchTypes)) return;
			CardsList = null; //reset

			//loading........
			LoadingText = "(loading data.....)";

			//split ids based on space
			string[] types = searchTypes.Replace(" ", String.Empty).Split(',');

			//fill list with results 
			List<Card> searchResults = new List<Card>();

			foreach (string typeStr in types)
			{

				//'search' cards by type in local cards repository
				//=> TODO: call async!
				var cards = LocalCardsRepository.GetCardsOfType(typeStr.Trim());

				searchResults.AddRange(cards);

			}

			//replace original list by new filtered list
			CardsList = searchResults;
			LoadingText = string.Empty;
		}

	}
