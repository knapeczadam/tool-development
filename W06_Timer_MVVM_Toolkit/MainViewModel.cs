using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace W06_Timer_MVVM_Toolkit;


    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _counter;

        private DispatcherTimer _timer;
        private DateTime _startTime;

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
        }

        [RelayCommand]
        private void StartTimer()
        {
            _startTime = DateTime.Now;
            _timer.Start();
        }

        [RelayCommand]
        private void StopTimer()
        {
            _timer.Stop();
        }
    }
