using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ImageProcessorToolkit
{
    public delegate void ImageProcessorPropertyChanged(object sender, string propertyName, object? oldValue, object? newValue);

    public class ImageProcessorSettings : INotifyPropertyChanged
    {
        private bool _grayscale;
        [JsonProperty("grayscale")]
        public bool Grayscale
        {
            get => _grayscale;
            set => SetField(ref _grayscale, value);
        }

        private int _hueShift;
        [JsonProperty("hue_shift")]
        public int HueShift
        {
            get => _hueShift;
            set => SetField(ref _hueShift, int.Clamp(value, 0, 360));
        }

        private double _saturation = 1;
        [JsonProperty("saturation")]
        public double Saturation
        {
            get => _saturation;
            set => SetField(ref _saturation, double.Clamp(value,-10,10));
        }

        private string _overlayColor = "Red";
        [JsonProperty("overlay_color")]
        public string OverlayColor
        {
            get => _overlayColor;
            set => SetField(ref _overlayColor, value);
        }

        private byte _overlayAlpha;
        [JsonProperty("overlay_alpha")]
        public byte OverlayAlpha
        {
            get => _overlayAlpha;
            set => SetField(ref _overlayAlpha, value);
        }

        public static readonly string[] AvailableOverlayColors =
        [
            "Red",
            "Blue",
            "Green"
        ];

        public void Reset()
        {
            Grayscale = false;
            HueShift = 0;
            Saturation = 1;
            OverlayColor = AvailableOverlayColors.First();
            OverlayAlpha = 0;
        }

        public void CopyFrom(ImageProcessorSettings other)
        {
            Grayscale = other.Grayscale;
            HueShift = other.HueShift;
            Saturation = other.Saturation;
            OverlayColor = other.OverlayColor;
            OverlayAlpha = other.OverlayAlpha;
        }

        #region Helpers
        internal bool TryConvertOverlayColor(out Color color)
        {
            var result = ColorConverter.ConvertFromString(OverlayColor);
            if (result == null) return false;

            if (result is Color cResult)
            {
                color = Color.FromArgb(OverlayAlpha, cResult.R, cResult.G, cResult.B);
                return true;
            }

            return false;
        }

        #region Change Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ImageProcessorPropertyChanged? OnSettingChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            T oldValue = field;
            field = value;
            OnPropertyChanged(propertyName);
            OnSettingChanged?.Invoke(this, propertyName, oldValue, value);
            return true;
        }
        #endregion
        #endregion
    }
}
