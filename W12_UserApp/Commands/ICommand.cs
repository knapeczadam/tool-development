using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W12_UserApp.Commands
{
    interface ICommand
    {
        void Execute();

        void Undo();

        void Redo()
        {
            Execute(); // Default implementation for Redo, can be overridden if needed
        }
    }
}
