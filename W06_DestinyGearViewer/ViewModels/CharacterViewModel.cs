using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W06_DestinyGearViewer.DataProviders;
using W06_DestinyGearViewer.Misc;
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.ViewModels
{
    internal partial class CharacterViewModel : ObservableObject
    {
        [ObservableProperty] private DestinyCharacter _characterData;

        public CharacterViewModel()
        {
            if (Helpers.IsDesignMode)
            {
                CharacterData = DesignTimeData.MockCharacter;
            }
        }

        [RelayCommand]
        private void OnItemClicked(DestinyItem item)
        {
            MessageBox.Show(Application.Current.MainWindow, item.ItemDescription, $"{item.ItemName} [{item.TierType}]");
        }
    }
}
