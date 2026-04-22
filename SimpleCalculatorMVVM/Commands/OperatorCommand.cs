using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Commands
{
    public class OperatorCommand : ICalculatorCommand
    {
        private readonly CalculatorModel _model;
        private readonly string _operation;
        private string _previousInput;
        private string _previousOperation;
        private double _previousFirstNumber;
        private bool _previousOperatorPressed;

        public OperatorCommand(CalculatorModel model, string operation)
        {
            _model = model;
            _operation = operation;
        }

        public void Execute()
        {
            // Сохраняем состояние
            _previousInput = _model.CurrentInput;
            _previousOperation = _model.CurrentOperation;
            _previousFirstNumber = _model.FirstNumber;
            _previousOperatorPressed = _model.IsOperatorJustPressed;

            // Выполняем команду
            if (!_model.IsOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(_model.CurrentOperation))
                {
                    _model.CalculateResult();
                }
                else
                {
                    _model.FirstNumber = double.Parse(_model.CurrentInput);
                }
            }

            _model.CurrentOperation = _operation;
            _model.IsOperatorJustPressed = true;
        }

        public void Undo()
        {
            _model.CurrentInput = _previousInput;
            _model.CurrentOperation = _previousOperation;
            _model.FirstNumber = _previousFirstNumber;
            _model.IsOperatorJustPressed = _previousOperatorPressed;
        }

        public string Description => $"Оператор {_operation}";
    }
}