using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace ImageProcessorToolkit
{
    public static class ImageLoader
    {
        /// <summary>
        /// Loads an image from the specified file path into a BitmapImage with full caching and thread safety.
        /// </summary>
        public static ImageSource LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Image file not found.", filePath);

            // Read all bytes into memory first
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // Load from memory stream (so file is not locked)
            using var stream = new MemoryStream(imageBytes);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        /// <summary>
        /// Loads an image from the specified file path (with OpenFileDialog) into a BitmapImage with full caching and thread safety.
        /// </summary>
        public static ImageSource? LoadFromFile()
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff)|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff|All Files (*.*)|*.*"
            };

            if (ofd.ShowDialog() != true)
                return null;

            return LoadFromFile(ofd.FileName);
        }
    }
}
