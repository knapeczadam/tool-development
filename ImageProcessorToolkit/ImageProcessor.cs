
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace ImageProcessorToolkit
{
    public class ImageProcessor
    {
        private readonly object _lock = new();
        private CancellationTokenSource? _debounceCts;
        private DateTime _lastAppliedAt = DateTime.MinValue;
        private readonly int _minIntervalMs;

        private ImageSource? _latestInput;
        private ImageProcessorSettings? _latestSettings;
        private long _lastCallId;
        private const float Tolerance = 0.0001f;

        public ImageProcessor(int minIntervalMs = 100)
        {
            _minIntervalMs = minIntervalMs;
        }

        /// <summary>
        /// Applies effects asynchronously with debounce.
        /// Returns null if throttled, cancelled.
        /// </summary>
        public Task<ImageSource?> ApplyAsync(ImageSource input, ImageProcessorSettings settings)
        {
            CancellationTokenSource localCts;
            long callId;

            lock (_lock)
            {
                _latestInput = input;
                _latestSettings = settings;

                _debounceCts?.Cancel();
                _debounceCts = new CancellationTokenSource();
                localCts = _debounceCts;

                callId = ++_lastCallId;
            }

            return Task.Run(async () =>
            {
                try
                {
                    var elapsed = DateTime.UtcNow - _lastAppliedAt;
                    if (elapsed.TotalMilliseconds < _minIntervalMs)
                    {
                        await Task.Delay(_minIntervalMs - (int)elapsed.TotalMilliseconds, localCts.Token);
                    }

                    lock (_lock)
                    {
                        if (callId != _lastCallId || localCts.Token.IsCancellationRequested)
                            return null;

                        if (_latestInput is null || _latestSettings is null)
                            return null;

                        _lastAppliedAt = DateTime.UtcNow;
                        return ApplyInternal(_latestInput, _latestSettings);
                    }
                }
                catch (TaskCanceledException)
                {
                    return null;
                }
            });
        }

        private static ImageSource ApplyInternal(ImageSource input, ImageProcessorSettings settings)
        {
            if (input is not BitmapSource bitmap)
                throw new ArgumentException("Input must be a BitmapSource", nameof(input));

            var wb = new WriteableBitmap(bitmap);
            int width = wb.PixelWidth;
            int height = wb.PixelHeight;
            int stride = width * 4;
            byte[] pixels = new byte[height * stride];
            wb.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];

                // Grayscale
                if (settings.Grayscale)
                {
                    byte gray = (byte) (0.3 * r + 0.59 * g + 0.11 * b);
                    r = g = b = gray;
                }

                // Hue Shift
                if (settings.HueShift != 0)
                {
                    double angle = settings.HueShift * Math.PI / 180.0;
                    double cosA = Math.Cos(angle);
                    double sinA = Math.Sin(angle);

                    double newR = r * cosA - g * sinA;
                    double newG = r * sinA + g * cosA;

                    r = ClampToByte(newR);
                    g = ClampToByte(newG);
                }

                // Saturation
                if (Math.Abs(settings.Saturation - 1.0) > Tolerance)
                {
                    double avg = (r + g + b) / 3.0;
                    r = ClampToByte(avg + (r - avg) * settings.Saturation);
                    g = ClampToByte(avg + (g - avg) * settings.Saturation);
                    b = ClampToByte(avg + (b - avg) * settings.Saturation);
                }

                // Overlay Color
                if (settings.OverlayAlpha > 0)
                {
                    if (settings.TryConvertOverlayColor(out Color c))
                    {
                        double blend = c.A / 255.0;
                        r = ClampToByte((1 - blend) * r + blend * c.R);
                        g = ClampToByte((1 - blend) * g + blend * c.G);
                        b = ClampToByte((1 - blend) * b + blend * c.B);
                    }
                }

                pixels[i] = b;
                pixels[i + 1] = g;
                pixels[i + 2] = r;
                pixels[i + 3] = a;
            }

            var result = new WriteableBitmap(width, height, wb.DpiX, wb.DpiY, PixelFormats.Bgra32, null);
            result.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            result.Freeze();
            return result;
        }

        private static byte ClampToByte(double value) => (byte)Math.Max(0, Math.Min(255, value));
    }
}
