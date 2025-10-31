using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W09_LabTest.Model;

namespace W09_LabTest.Repository
{
    /// <summary>
    /// Repository for loading apex legend data from json files in the resources.
    /// This class doesn't require any modifications for the assignment.
    /// </summary>
    internal class ApexLegendsRepository
    {
        private const string _mapsResourcePath = "W09_LabTest.Resources.Data.maps.json";
        private const string _legendsResourcePath = "W09_LabTest.Resources.Data.legends.json";

        //CACHED DATA
        private static List<Map>? _cachedMaps;
        private static List<Legend>? _cachedLegends;

        //PUBLIC METHODS
        public void PreLoad()
        {
            _cachedMaps = FetchMaps();
            _cachedLegends = FetchLegends();
        }
		public List<Legend> GetAllLegends(bool forceReload = false)
		{
			if (forceReload || _cachedLegends == null)
				_cachedLegends = FetchLegends();

			return _cachedLegends;
		}
		public List<Map> GetAllMaps(bool forceReload = false)
		{
			if (forceReload || _cachedMaps == null)
				_cachedMaps = FetchMaps();

			return _cachedMaps;
		}

        //PRIVATE METHODS
		private List<Map> FetchMaps()
        {
           return LoadData<List<Map>>(_mapsResourcePath) ?? new();
        }

        private List<Legend> FetchLegends()
        {
            return LoadData<List<Legend>>(_legendsResourcePath) ?? new();
        }

        private static T? LoadData<T>(string resourcePath)
        {
            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (stream == null) return default;

            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
        }
    }
}
