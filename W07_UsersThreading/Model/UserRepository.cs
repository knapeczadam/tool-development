using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace W07_UsersThreading.Model;


    internal class UserRepository
    {
        private List<User> _allUsers = new List<User>();
        public List<int> GetAllUserIds()
        {
            LoadUsersFromFile();
            return _allUsers.Select(u => u.Id).ToList();

		}
        
        public User GetUser(int id)
        {
            Thread.Sleep(10); //Take some extra time to look up user
            LoadUsersFromFile();
            return _allUsers.First(u => u.Id == id);
        }

        public void RemoveUser(int id)
        {
            _allUsers.RemoveAll(u => u.Id == id);
        }


		private void LoadUsersFromFile()
		{
			if (_allUsers.Any()) return;
			string txt = File.ReadAllText("Resources/users.json");
			_allUsers = JsonConvert.DeserializeObject<List<User>>(txt) ?? [];
		}

	}
