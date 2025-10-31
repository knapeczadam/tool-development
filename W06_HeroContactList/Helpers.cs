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

namespace W06_HeroContactList;


    internal static class Helpers
    {
        // Check if the application is in Design Mode
        public static bool IsDesignMode => (bool)(DesignerProperties.IsInDesignModeProperty
            .GetMetadata(typeof(DependencyObject)).DefaultValue);

        // Get the Project directory
        public static string ProjectDir => GetProjectDir_Internal();

        private static string GetProjectDir_Internal([CallerFilePath] string path = "")
        {
            return Directory.GetParent(path).FullName;
        }

        public static ImageSource GetImageSource(string path)
        {
            if (IsDesignMode)
            {
                path = Path.Combine(ProjectDir, path);
            }

            if (!File.Exists(path)) return null;

            var imageData = File.ReadAllBytes(path);
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = new MemoryStream(imageData);
            imageSource.EndInit();
            imageSource.Freeze();

            return imageSource;
        }
    }
