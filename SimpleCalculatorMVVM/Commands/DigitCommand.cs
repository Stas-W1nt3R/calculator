using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Commands
{
    public class DigitCommand : ICalculatorCommand
    {
        private readonly CalculatorModel _model;
        private readonly string _digit;
        private string _previousInput;
        private bool _previousNewCalculation;
        private bool _previousOperatorPressed;

        public DigitCommand(CalculatorModel model, string digit)
        {
            _model = model;
            _digit = digit;
        }

        public void Execute()
        {
            // Сохраняем состояние для Undo
            _previousInput = _model.CurrentInput;
            _previousNewCalculation = _model.IsNewCalculation;
            _previousOperatorPressed = _model.IsOperatorJustPressed;

            // Выполняем команду
            if (_model.IsNewCalculation || _model.CurrentInput == "0" || _model.IsOperatorJustPressed)
            {
                _model.CurrentInput = _digit;
                _model.IsNewCalculation = false;
                _model.IsOperatorJustPressed = false;
            }
            else
            {
                _model.CurrentInput += _digit;
            }
        }

        public void Undo()
        {
            _model.CurrentInput = _previousInput;
            _model.IsNewCalculation = _previousNewCalculation;
            _model.IsOperatorJustPressed = _previousOperatorPressed;
        }

        public string Description => $"Ввод цифры {_digit}";
    }
}