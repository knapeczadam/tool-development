using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace W06_Timer_MVVM_Toolkit_Notify;


    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _counter;

        private DispatcherTimer _timer;
        private DateTime _startTime;


        [NotifyCanExecuteChangedFor(nameof(StartTimerCommand), nameof(StopTimerCommand))]
        [ObservableProperty]
        private bool _isRunning;

        public MainViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _timer.Tick += (o, e) =>
            {
                var elapsed = DateTime.Now - _startTime;
                Counter = elapsed.TotalSeconds;
            };

            _startTime = DateTime.Now;
        }

        [RelayCommand(CanExecute = nameof(CanStartTimer))]
        private void StartTimer()
        {
            _startTime = DateTime.Now;
            _timer.Start();

            IsRunning = true;

        }

        private bool CanStartTimer() => !_timer.IsEnabled;

        [RelayCommand(CanExecute = nameof(CanStopTimer))]
        private void StopTimer()
        {
            _timer.Stop();

            IsRunning = false;
        }

        private bool CanStopTimer() => _timer.IsEnabled;
    }
