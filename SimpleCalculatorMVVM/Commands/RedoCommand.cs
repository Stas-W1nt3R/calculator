using SimpleCalculatorMVVM.CommandManager;

namespace SimpleCalculatorMVVM.Commands
{
    public class RedoCommand : ICalculatorCommand
    {
        private readonly CommandInvoker _invoker;

        public RedoCommand(CommandInvoker invoker)
        {
            _invoker = invoker;
        }

        public void Execute()
        {
            _invoker.Redo();
        }

        public void Undo()
        {
            // Redo нельзя отменить стандартным способом
        }

        public string Description => "Повтор действия";
    }
}