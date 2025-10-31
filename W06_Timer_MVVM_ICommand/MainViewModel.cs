using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace W06_Timer_MVVM_ICommand;


    class MainViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private DateTime _startTime = DateTime.Now;

        public DispatcherTimer Timer => _timer;
        public DateTime StartTime 
        {
            get => _startTime;
            set => _startTime = value;
        }

        public StartTimerCommandImpl StartTimerCommand { get; }
        public StopTimerCommandImpl StopTimerCommand { get; }

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


        public MainViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _timer.Tick += (s, e) =>
            {
                var diff = DateTime.Now - _startTime;
                Counter = diff.TotalSeconds;
            };

            StartTimerCommand = new StartTimerCommandImpl(this);
            StopTimerCommand = new StopTimerCommandImpl(this);
        }

    }
