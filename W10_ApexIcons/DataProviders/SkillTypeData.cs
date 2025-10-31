using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace W10_ApexIcons.DataProviders
{
	public static class SkillTypeData
	{
		public static List<string> AllSkillTypes = new List<string>();

		static SkillTypeData()
		{
			var rm = new ResourceManager("W10_ApexIcons.g", Assembly.GetExecutingAssembly());
			var resources = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
			foreach (var res in resources)
			{
				if (res is DictionaryEntry entry)
				{
					string? path = entry.Key.ToString();
					if (string.IsNullOrEmpty(path)) continue;
					if (path.EndsWith(".png"))
					{
						path = path.Replace("resources/", "");
						path = path.Replace(".png", "");
						AllSkillTypes.Add(path);

					}
				}
			}
		}
	}
}
