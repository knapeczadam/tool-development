using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace W10_ColorSwatches.Commands
{
    public class UndoManager
    {
        private Stack<Color> _undoStack = new Stack<Color>();
        private Stack<Color> _redoStack = new Stack<Color>();
        
        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Any();

        public void SaveSnapshot(Color state)
        {
            _undoStack.Push(state);
            _redoStack.Clear();
        }

        public Color Undo()
        {
            if (_undoStack.Count > 0)
            {
                var state = _undoStack.Pop();
                _redoStack.Push(state);
                
                return _undoStack.Peek();
            }

            return default;
        }

        public Color Redo()
        {
            if (_redoStack.Any())
            {
                var state = _redoStack.Pop();
                _undoStack.Push(state);

                return state;
            }
            return default;
        }
    }
}
