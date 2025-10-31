using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;
using System.Security.Policy;
using CommunityToolkit.Mvvm.ComponentModel;
using W10_ApexIcons.DataProviders;
using W10_ApexIcons.Models;

namespace W10_ApexIcons.ViewModels
{
	partial class IconEditorViewModel : ObservableObject
	{
		[ObservableProperty]
		private IconData _iconData;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _red;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _green;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _blue;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _alpha;

		public Color Color => Color.FromArgb(Alpha, Red,Green,Blue);

		[ObservableProperty]
		private string _name;

		[ObservableProperty]
		private string _skillType;

		public IconEditorViewModel()
		{
			Color c = Color.FromArgb(255, 255, 0, 0);
			_iconData = new IconData() { SkillType = SkillTypeData.AllSkillTypes.First(),Color = c , Name = "Skill" };

			//Sync RGBA Values
			_red = _iconData.Color.R;
			_green = _iconData.Color.G;
			_blue = _iconData.Color.B;
			_alpha = _iconData.Color.A;

			_name = _iconData.Name;
			_skillType = _iconData.SkillType;
		}

		//Hooks
		partial void OnRedChanged(int value)
		{
			IconData.Color = Color.FromArgb(Alpha, Red, Green, Blue);
		}

		partial void OnGreenChanged(int value)
		{
			IconData.Color = Color.FromArgb(Alpha, Red, Green, Blue);
		}

		partial void OnBlueChanged(int value)
		{
			IconData.Color = Color.FromArgb(Alpha, Red, Green, Blue);
		}
		partial void OnAlphaChanged(int value)
		{
			IconData.Color = Color.FromArgb(Alpha, Red, Green, Blue);
		}

		partial void OnNameChanged(string value)
		{
			IconData.Name = value;
		}

		partial void OnSkillTypeChanged(string value)
		{
			IconData.SkillType = value;
		}
	}
}
