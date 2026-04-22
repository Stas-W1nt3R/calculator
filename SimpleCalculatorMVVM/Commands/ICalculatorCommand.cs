using System;

namespace SimpleCalculatorMVVM.Commands
{
    // Интерфейс команды
    public interface ICalculatorCommand
    {
        void Execute();
        void Undo();
        string Description { get; }
    }
}