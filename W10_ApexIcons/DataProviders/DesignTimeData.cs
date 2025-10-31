using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W10_ApexIcons.Models;

namespace W10_ApexIcons.DataProviders
{
	static class DesignTimeData
	{
		public static IconData MockIconData
		{
			get
			{
				return new IconData() { SkillType = "Assassins_Instinct.png", Color = Color.Red, Name = "Icon Name Here" };
			}
		}
	}
}
