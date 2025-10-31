using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace W06_Timer_MVVM_ICommand;


    class StopTimerCommandImpl(MainViewModel vm) : ICommand
    {
        private MainViewModel _vm = vm;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return _vm.Timer.IsEnabled;
        }

        public void Execute(object? parameter)
        {
            _vm.Timer.Stop();
        }
    }
