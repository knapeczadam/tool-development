using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W07_UsersThreading.Model;

namespace W07_UsersThreading.ViewModel;


	internal partial class MainViewModel : ObservableObject
	{
		public ObservableCollection<User> AllUsers { get; set; } = new ObservableCollection<User>();

		[RelayCommand]
		void LoadAllUsers()
		{
			UserRepository repository = new UserRepository();
			var userIds = repository.GetAllUserIds();

			foreach (var userId in userIds)
			{
				User user = repository.GetUser(userId)!;
				AllUsers.Add(user);
			}
		}
		
	}
