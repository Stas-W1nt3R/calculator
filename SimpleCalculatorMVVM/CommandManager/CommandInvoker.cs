using System.Collections.Generic;
using SimpleCalculatorMVVM.Commands;

namespace SimpleCalculatorMVVM.CommandManager
{
    // Инвокер - управляет выполнением команд
    public class CommandInvoker
    {
        private readonly Stack<ICalculatorCommand> _undoStack = new Stack<ICalculatorCommand>();
        private readonly Stack<ICalculatorCommand> _redoStack = new Stack<ICalculatorCommand>();

        private const int MaxHistorySize = 50;

        public void ExecuteCommand(ICalculatorCommand command)
        {
            command.Execute();

            _undoStack.Push(command);
            _redoStack.Clear();

            while (_undoStack.Count > MaxHistorySize)
            {
                _undoStack.Pop();
            }
        }

        public ICalculatorCommand Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                return command;
            }
            return null;
        }

        public ICalculatorCommand Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                return command;
            }
            return null;
        }

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public string GetUndoDescription()
        {
            if (_undoStack.Count > 0)
                return $"Отменить: {_undoStack.Peek().Description}";
            return "Нечего отменять";
        }

        public string GetRedoDescription()
        {
            if (_redoStack.Count > 0)
                return $"Повторить: {_redoStack.Peek().Description}";
            return "Нечего повторять";
        }

        public void ClearHistory()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}