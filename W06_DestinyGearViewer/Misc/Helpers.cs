using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using W06_DestinyGearViewer.DataProviders;

namespace W06_DestinyGearViewer.Misc
{
    internal static class Helpers
    {
        public static bool IsDesignMode => (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue);

        public static string ProjectDir => GetProjectDir_Internal();

        public static IDestinyDataProvider DataProvider => App.Current.Resources["dataProvider"] as IDestinyDataProvider;

        private static string GetProjectDir_Internal([CallerFilePath] string path = "")
        {
            return Directory.GetParent(path).Parent.FullName;
        }

        public static ImageSource ToImageSource(byte[] data)
        {
            var imageData = new BitmapImage();
            imageData.BeginInit();
            imageData.StreamSource = new MemoryStream(data);
            imageData.EndInit();
            imageData.Freeze();

            return imageData;
        }
    }
}
