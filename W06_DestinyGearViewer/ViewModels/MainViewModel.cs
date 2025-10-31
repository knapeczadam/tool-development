using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W06_DestinyGearViewer.Misc;
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private DestinyPlayer _playerData;

        [ObservableProperty]
        private DestinyCharacter _selectedCharacter;

        [ObservableProperty] 
        private string _playerName;

        public MainViewModel()
        {
            if (Helpers.IsDesignMode)
            {
                //Design-time data
                PlayerData = Helpers.DataProvider.GetMockData();
                PlayerName = PlayerData?.DisplayName ?? "LOAD FAIL";
                SelectedCharacter = PlayerData?.Characters[0];
            }
            else
            {
                LoadPlayer("ToMiHa");
            }
        }

        [RelayCommand]
        private void LoadPlayer(string playerName)
        {
            var data = Helpers.DataProvider.GetPlayerData(playerName);
            if (data == null)
            {
                MessageBox.Show(App.Current.MainWindow, $"Player \'{playerName}\' not found...", "Not Found");
                PlayerName = PlayerData?.DisplayName;
                return;
            }

            PlayerData = data;
            PlayerName = PlayerData.DisplayName;
            SelectedCharacter = PlayerData.Characters[0];
        }
    }
}
