using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEffectEditor
{
    class CommandHistory
    {
        public event EventHandler? StacksChanged;
  
        private LinkedList<ICommand> _undoStack = new LinkedList<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();
  
        public bool CanUndo => _undoStack.Any();
        public bool CanRedo => _redoStack.Any();
  
        private bool _preventRecording = false;
  
        public int StackLimit = 100;
  
        public void PushCommand(ICommand command)
        {
            if (_preventRecording) return;
  
            if (_undoStack.Count >= StackLimit)
            {
                _undoStack.RemoveFirst();
            }
            _undoStack.AddLast(command);
            _redoStack.Clear();
            
            StacksChanged?.Invoke(this, EventArgs.Empty);
        }
  
        public void Undo()
        {
            if (!CanUndo) return;
            
            ICommand command = _undoStack.Last();
            _undoStack.RemoveLast();
            _redoStack.Push(command);
  
            _preventRecording = true;
            command.Undo();
            _preventRecording = false;
  
			StacksChanged?.Invoke(this, EventArgs.Empty);
		}
  
        public void Redo()
        {
            if (!CanRedo) return;
  
			ICommand command = _redoStack.Pop();
            _undoStack.AddLast(command);
  
			_preventRecording = true;
            command.Execute();
            _preventRecording = false;
  
            StacksChanged?.Invoke(this, EventArgs.Empty);
        }
  
        public void Reset()
        {
            _undoStack.Clear();
            _redoStack.Clear();
  
            StacksChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
