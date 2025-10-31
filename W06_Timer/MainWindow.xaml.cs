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

namespace W06_Timer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static readonly DependencyProperty CounterProperty = DependencyProperty.Register(
        nameof(Counter), typeof(double), typeof(MainWindow), new PropertyMetadata(default(double))
        );
    public double Counter
    {
        get => (double)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    private DispatcherTimer _timer;
    private DateTime _startTime;

    public MainWindow()
    {
        InitializeComponent();
        Counter = 0;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        _timer.Tick += (s, e) =>
        {
            var diff = DateTime.Now - _startTime;
            Counter = diff.TotalSeconds;
        };
    }

    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        _startTime = DateTime.Now;
        _timer.Start();
    }

    private void BtnStop_Click(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }
}
