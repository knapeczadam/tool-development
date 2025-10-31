using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using W10_ColorSwatches.Commands;

namespace W10_ColorSwatches.ViewModel
{
	partial class MainViewModel : ObservableObject
	{
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(DeleteSwatchCommand))]
		private ObservableCollection<SwatchViewModel> _swatches = new ObservableCollection<SwatchViewModel>();

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(DeleteSwatchCommand))]
		private SwatchViewModel? _selectedSwatch;

		private UndoManager _undoRedo;

		public MainViewModel()
		{
			AddSwatch();
			SelectedSwatch = Swatches.First();
			
			_undoRedo = new UndoManager();
			_undoRedo.SaveSnapshot(SelectedSwatch.Color);
		}

		[RelayCommand]
		private void AddSwatch()
		{
			var swatch = new SwatchViewModel();
			Swatches.Add(swatch);

			SelectedSwatch = swatch;
			// _undoRedo.SaveSnapshot(swatch.Color);
		}

		[RelayCommand(CanExecute = nameof(CanDeleteSwatch))]
		private void DeleteSwatch(SwatchViewModel swatch)
		{
			Swatches.Remove(swatch);
			SelectedSwatch = null;
			// _undoRedo.SaveSnapshot(swatch.Color);
		}
		public bool CanDeleteSwatch(SwatchViewModel? swatch)
		{
			return SelectedSwatch != null && Swatches.Count > 0;
		}

		//Undo / Redo
		public bool CanRedo => _undoRedo.CanRedo;
		public bool CanUndo => _undoRedo.CanUndo;

		[RelayCommand(CanExecute = nameof(CanUndo))]
		private void Undo()
		{
			Color prevColor = _undoRedo.Undo();
			SelectedSwatch.Red = prevColor.R;
			SelectedSwatch.Green = prevColor.G;
			SelectedSwatch.Blue = prevColor.B;
			SelectedSwatch.Alpha = prevColor.A;
			
			
			
			UndoCommand.NotifyCanExecuteChanged();
			RedoCommand.NotifyCanExecuteChanged();
		}

		[RelayCommand(CanExecute = nameof(CanRedo))]
		private void Redo()
		{

			Color prevColor = _undoRedo.Redo();
			SelectedSwatch.Red = prevColor.R;
			SelectedSwatch.Green = prevColor.G;
			SelectedSwatch.Blue = prevColor.B;
			SelectedSwatch.Alpha = prevColor.A;
			
			UndoCommand.NotifyCanExecuteChanged();
			RedoCommand.NotifyCanExecuteChanged();
		}
	}
}
