using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W06_HeroContactList.Models;

namespace W06_HeroContactList.ViewModels;


    internal partial class EditViewModel : ObservableObject
    {
        [ObservableProperty]
        private HeroContact _targetContact;

        [ObservableProperty]
        private string _newFirstName;

        [ObservableProperty]
        private string _newLastName;

        public EditViewModel()
        {
            if (Helpers.IsDesignMode)
            {
                var service = (IHeroContactService)App.Current.Resources["heroContactService"];
                var mockContacts = service.LoadMockContacts();

                TargetContact = mockContacts.FirstOrDefault();
            }
        }

        partial void OnTargetContactChanged(HeroContact value)
        {
            NewFirstName = value.FirstName;
            NewLastName = value.LastName;
        }

        [RelayCommand(CanExecute = nameof(CanApplyChanges))]
        private void ApplyChanges(Window target)
        {
            TargetContact.FirstName = NewFirstName;
            TargetContact.LastName = NewLastName;

            target.Close();
        }

        private bool CanApplyChanges()
        {
            return !string.IsNullOrWhiteSpace(NewFirstName) && !string.IsNullOrWhiteSpace(NewLastName);
        }
    }
