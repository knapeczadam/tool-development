using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using W12_UserApp.Model;

namespace W12_UserApp.Services
{
    static class DialogService
    {
        public static string? OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml",
                Multiselect = false
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public static string? SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml",
                DefaultExt = ".json"
            };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }
    }
}
