using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W12_UserApp.Model;
using W12_UserApp.ViewModel;

namespace W12_UserApp.Commands
{
    class AddUserCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        private User _lastUser;

        public AddUserCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute()
        {
            _viewModel.Users.Add(_viewModel.NewUser);
            _viewModel.NewUser = new User(); // Reset NewUser after adding
        }

        public void Undo()
        {
            // Assuming the last added user is the one to be removed
            if (_viewModel.Users.Any())
            {
                _lastUser = _viewModel.Users.Last();
                _viewModel.Users.Remove(_lastUser);
                _viewModel.NewUser = _lastUser; // Restore the last user to NewUser
            }
        }

        public void Redo()
        {
            // Re-add the last user that was removed
            if (_lastUser != null)
            {
                _viewModel.Users.Add(_lastUser);
                _viewModel.NewUser = new User(); // Reset NewUser after re-adding
            }
        }
    }
}
