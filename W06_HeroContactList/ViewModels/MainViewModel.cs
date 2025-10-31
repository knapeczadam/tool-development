using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W06_HeroContactList.Models;
using W06_HeroContactList.Views;

namespace W06_HeroContactList.ViewModels;


    internal partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<HeroContact> _contacts;

        private IHeroContactService _heroContactService;

        public MainViewModel()
        {
            _heroContactService = (IHeroContactService)App.Current.Resources["heroContactService"];

            if (Helpers.IsDesignMode)
            {
                var mockContacts = _heroContactService.LoadMockContacts();
                Contacts = new ObservableCollection<HeroContact>(mockContacts);
            }
        }

        [RelayCommand]
        private void RefreshContact()
        {
            var contacts = _heroContactService.LoadContacts("Resources/Contacts.json");
            Contacts = new ObservableCollection<HeroContact>(contacts);
        }

        [RelayCommand]
        private void EditContact(HeroContact contact)
        {
            var editWindow = new EditView();
            var editVM = App.Current.Resources["editVM"] as EditViewModel;
            editVM.TargetContact = contact;

            editWindow.Owner = App.Current.MainWindow;
            editWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            editWindow.ShowDialog();
        }
    }
