using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W09_LabTest.Model;
using W09_LabTest.Repository;

namespace W09_LabTest.ViewModel
{
	internal partial class MainViewModel : ObservableObject
	{
		private ApexLegendsRepository _apexRepository;
		private MatchRepository _matchRepository;

		//Matches
		[ObservableProperty]
		private ObservableCollection<MatchViewModel> _matches = new();

		[ObservableProperty]
		private MatchViewModel? _selectedMatch;

		public MainViewModel()
		{
			//Load ApexRepository
			_apexRepository = new ApexLegendsRepository();
			_apexRepository.PreLoad();

			//Load MatchRepository
			_matchRepository = new MatchRepository();

			//Create default match
			AddMatch();
		}

		[RelayCommand]
		public void AddMatch()
		{
			//generate name
			int id = 0;
			string name;
			do
			{
				++id;
				name = $"Match {id: 00}";
			}
			while (Matches.Any(m => m.MatchName == name));

			Match matchModel = new Match() { Name = name };
			MatchViewModel vm = new MatchViewModel(matchModel, _apexRepository);
			Matches.Add(vm);

			//Auto select new match
			SelectedMatch = vm;
		}

		[RelayCommand]
		public void RemoveMatch(MatchViewModel? matchVM)
		{
			if (matchVM == null) return;
			Matches.Remove(matchVM);
			SelectedMatch = Matches.FirstOrDefault();
		}

		//TODO: bind to the load menu item (File >> Load)
		[RelayCommand]
		public async Task LoadMatches()
		{
			//path > Load-path to Match data (from json)
			string? path = DialogService.OpenFile();
			if (path == null) return;

			// TODO: Load matches using the match-repository (_matchRepository)
			Matches.Clear();
			
			var matches = await _matchRepository.LoadMatches(path);
			foreach (var matchModel in matches)
			{
				MatchViewModel vm = new MatchViewModel(matchModel, _apexRepository);
				Matches.Add(vm);
			}

			// TODO: Automatically select the first match
			SelectedMatch = Matches.FirstOrDefault();
		}

		//TODO: bind to the save menu item (File >> Save)
		[RelayCommand]
		public async Task SaveMatches()
		{
			//path > Save-path for Match data (to json)
			string? path = DialogService.SaveFile();
			if (path == null) return;

			// TODO: Save matches using the match-repository (_matchRepository)
			var matches = Matches.Select(vm => vm.MatchData).ToList();
			await _matchRepository.SaveMatches(matches, path);
		}

	}
}
