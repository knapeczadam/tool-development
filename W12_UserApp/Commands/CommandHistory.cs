using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W12_UserApp.Commands
{
    class CommandHistory
    {
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        public bool CanUndo => _undoStack.Any();
        public bool CanRedo => _redoStack.Any();

        private bool _isExecutingCommand = false;

        public void ExecuteCommand(ICommand command)
        {
            if (_isExecutingCommand) return; // Prevent re-entrancy

            _undoStack.Push(command);
            _redoStack.Clear(); // Clear redo stack on new command execution

            _isExecutingCommand = true;
            command.Execute();
            _isExecutingCommand = false;
        }

        public void Undo()
        {
            if (!CanUndo) return;

            var command = _undoStack.Pop();
            _redoStack.Push(command);

            _isExecutingCommand = true;
            command.Undo(); // Assuming ICommand has an Undo method
            _isExecutingCommand = false;
        }

        public void Redo()
        {
            if (!CanRedo) return;

            var command = _redoStack.Pop();
            _undoStack.Push(command);

            _isExecutingCommand = true;
            command.Redo(); // Re-execute the command
            _isExecutingCommand = false;
        }
    }
}
