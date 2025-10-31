using Microsoft.Win32;

namespace ToolDevExam2.Helpers
{
	static class DialogService
	{
		public static string? OpenFile()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "JSON files (*.json)|*.json",
				Multiselect = false
			};

			return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
		}

		public static string? SaveFile(string defaultFileName = "")
		{
			SaveFileDialog dialog = new SaveFileDialog()
			{
				Filter = "JSON files (*.json)|*.json",
				FileName = defaultFileName
			};
			return dialog.ShowDialog() == true ? dialog.FileName : null;
		}
	}
}
