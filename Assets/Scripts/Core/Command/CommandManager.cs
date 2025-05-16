using System;
using System.Collections.Generic;

namespace Solitaire.Core.Command
{
    public class CommandManager
    {
        private static CommandManager _instance;
        public static CommandManager Instance
        {
            get
            {
                _instance ??= new CommandManager();
                return _instance;
            }
        }

        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        
        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.Execute();
            _undoStack.Push(command);
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
            }
        }

        public bool CanUndo => _undoStack.Count > 0;

        public static void Reset()
        {
            _instance = null;
        }
    }
} 