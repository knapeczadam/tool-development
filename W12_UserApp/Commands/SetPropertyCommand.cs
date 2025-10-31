using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W12_UserApp.Commands
{
    class SetPropertyCommand<T> : ICommand
    {
        private readonly Action<T> _setPropertyAction;
        private readonly T _oldValue;
        private readonly T _newValue;

        public SetPropertyCommand(Action<T> setPropertyAction, T oldValue, T newValue)
        {
            _setPropertyAction = setPropertyAction ?? throw new ArgumentNullException(nameof(setPropertyAction));
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public void Execute()
        {
            _setPropertyAction(_newValue);
        }

        public void Undo()
        {
            _setPropertyAction(_oldValue);
        }
    }
}
