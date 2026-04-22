using SimpleCalculatorMVVM.CommandManager;

namespace SimpleCalculatorMVVM.Commands
{
    public class UndoCommand : ICalculatorCommand
    {
        private readonly CommandInvoker _invoker;
        private ICalculatorCommand _undoneCommand;

        public UndoCommand(CommandInvoker invoker)
        {
            _invoker = invoker;
        }

        public void Execute()
        {
            _undoneCommand = _invoker.Undo();
        }

        public void Undo()
        {
            if (_undoneCommand != null)
                _invoker.Redo();
        }

        public string Description => "Отмена последнего действия";
    }
}