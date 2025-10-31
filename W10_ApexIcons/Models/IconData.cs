using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W10_ApexIcons.Models
{
	public partial class IconData : ObservableObject
	{
		[ObservableProperty]
		private string _skillType = "";
		[ObservableProperty]
		private string _name = "";
		[ObservableProperty]
		private Color _color;
	}
}
