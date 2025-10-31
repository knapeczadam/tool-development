using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace W06_Timer_MVVM_RelayCommand;


    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private double _counter;

        public double Counter
        {
            get => _counter;
            set
            {
                _counter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Counter)));
            }
        }

        private DispatcherTimer _timer;
        private DateTime _startTime = DateTime.Now;

        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }

        public MainViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _timer.Tick += (s, e) =>
            {
                var elapsed = DateTime.Now - _startTime;
                Counter = elapsed.TotalSeconds;
            };

            StartCommand = new RelayCommand(
                (param) =>
                {
                    _startTime = DateTime.Now;
                    _timer.Start();
                },
                _ => !_timer.IsEnabled
            );

            StopCommand = new RelayCommand(
                (param) =>
                {
                    _timer.Stop();
                },
                _ => _timer.IsEnabled
            );
        }

    }
