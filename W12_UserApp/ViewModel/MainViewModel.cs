using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W12_UserApp.Commands;
using W12_UserApp.Model;
using W12_UserApp.Repository;
using W12_UserApp.Services;

namespace W12_UserApp.ViewModel
{
    partial class MainViewModel : ObservableObject
    {
        private UserRepository _userRepository;
        private GenericFileService _genericFileService;

        [ObservableProperty]
        private User _newUser = new ();

        [ObservableProperty]
        private ObservableCollection<User> _users = [];

        public static CommandHistory CommandHistory { get; } = new CommandHistory();

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            _genericFileService = new GenericFileService();

            // Load initial users from the repository
            _userRepository.PreLoad();
            var initialUsers = _userRepository.GetAllUsers();
            foreach (var user in initialUsers)
            {
                Users.Add(user);
            }

            // Set up property change notifications for NewUser
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(NewUser))
                {
                    if (NewUser is INotifyPropertyChanged npc)
                    {
                        npc.PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == nameof(User.FirstName) || e.PropertyName == nameof(User.LastName))
                            {
                                OnPropertyChanged(nameof(CanAddUser));
                                AddUserCommand.NotifyCanExecuteChanged();
                            }
                        };
                    }
                }
            };
            if (NewUser is INotifyPropertyChanged npc)
            {
                npc.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(User.FirstName) || e.PropertyName == nameof(User.LastName))
                    {
                        OnPropertyChanged(nameof(CanAddUser));
                        AddUserCommand.NotifyCanExecuteChanged();
                    }
                };
            }
        }

        public bool CanAddUser => !string.IsNullOrWhiteSpace(NewUser.FirstName) && !string.IsNullOrWhiteSpace(NewUser.LastName);

        [RelayCommand(CanExecute = nameof(CanAddUser))]
        private void AddUser(User newUser)
        {
            CommandHistory.ExecuteCommand(new AddUserCommand(this));
        }

        [RelayCommand]
        private void Undo()
        {
            CommandHistory.Undo();
        }

        [RelayCommand]
        private void Redo()
        {
            CommandHistory.Redo();
        }

        [RelayCommand]
        private async Task LoadUsers()
        {
            string? path = DialogService.OpenFile();
            if (path == null) return;

            if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                var users = await _genericFileService.LoadJsonAsync<User>(path);
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            else if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var users = await _genericFileService.LoadXmlAsync<User>(path, "Users");
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }

        [RelayCommand]
        private async Task SaveUsers()
        {
            string? path = DialogService.SaveFile();
            if (path == null) return;

            if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                await _genericFileService.SaveJsonAsync(Users.ToList(), path);
            }
            else if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                await _genericFileService.SaveXmlAsync(Users.ToList(), path, "Users");
            }
        }
    }
}
