using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W12_UserApp.Model;

namespace W12_UserApp.Repository
{
    class UserRepository
    {
        private const string _usersResourcePath = "W12_UserApp.Resources.Data.users.json";

        //CACHED DATA
        private static List<User>? _cachedUsers;

        //PUBLIC METHODS
        public void PreLoad()
        {
            _cachedUsers = FetchUsers();
        }

		public List<User> GetAllUsers(bool forceReload = false)
		{
			if (forceReload || _cachedUsers == null)
				_cachedUsers = FetchUsers();

			return _cachedUsers;
		}

        //PRIVATE METHODS
		private List<User> FetchUsers()
        {
           return LoadData<List<User>>(_usersResourcePath) ?? [];
        }

        private static T? LoadData<T>(string resourcePath)
        {
            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (stream == null) return default;

            using StreamReader r = new StreamReader(stream);
            string json = r.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
    }
}
