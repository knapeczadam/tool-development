using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace W02_DrawContext;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Size _blockSize = new Size(40, 20);
    private Point _blockPosition = new Point(0, 200);
    private int _direction = 1;
    private const float SPEED = 200;

    private const int FRAMERATE = 60;
    private DateTime _prevDrawTime;

    public MainWindow()
    {
        InitializeComponent();
        _prevDrawTime = DateTime.Now;

        var paintTimer = new System.Timers.Timer
        {
            AutoReset = true,
            Interval = 1000.0 / FRAMERATE
        };

        paintTimer.Elapsed += (o, e) => Dispatcher.BeginInvoke(DispatcherPriority.Render, (Action)(InvalidateVisual));
        paintTimer.Start();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var now = DateTime.Now;
        var difference = now - _prevDrawTime;
        _prevDrawTime = now;
        var deltaTime = difference.TotalSeconds;

        // Move the block
        _blockPosition.X += _direction * SPEED * deltaTime;

        // Reverse direction when hitting window edges
        if (_blockPosition.X + _blockSize.Width >= RenderSize.Width)
        {
            _blockPosition.X = RenderSize.Width - _blockSize.Width;
            _direction *= -1;
        }
        else if (_blockPosition.X <= 0)
        {
            _blockPosition.X = 0;
            _direction *= -1;
        }
            // draw a white background
            drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, Width, Height));
        
        // draw a black block at a fixed position
        drawingContext.DrawRectangle(Brushes.Cyan, null, new Rect(_blockPosition, _blockSize));

        base.OnRender(drawingContext);
    }
}