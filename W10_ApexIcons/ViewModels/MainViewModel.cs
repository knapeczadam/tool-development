using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W10_ApexIcons.ViewModels
{
	partial class MainViewModel : ObservableObject
	{
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(DeleteIconCommand))]
		private ObservableCollection<IconEditorViewModel> _icons = new ObservableCollection<IconEditorViewModel>();

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(DeleteIconCommand))]
		private IconEditorViewModel? _selectedIcon;

		public MainViewModel()
		{
			AddIcon();
			SelectedIcon = Icons.First();
		}

		[RelayCommand]
		private void AddIcon()
		{
			var icon = new IconEditorViewModel();
			Icons.Add(icon);

			SelectedIcon = icon;
		}

		[RelayCommand(CanExecute = nameof(CanDeleteIcon))]
		private void DeleteIcon(IconEditorViewModel swatch)
		{
			Icons.Remove(swatch);
			SelectedIcon = null;
		}
		public bool CanDeleteIcon(IconEditorViewModel? swatch)
		{
			return SelectedIcon != null && Icons.Count > 0;
		}
	}
}
