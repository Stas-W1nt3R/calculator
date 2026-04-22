using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Commands
{
    public class FunctionCommand : ICalculatorCommand
    {
        private readonly CalculatorModel _model;
        private readonly string _functionType;
        private string _previousInput;
        private string _previousOperation;
        private double _previousFirstNumber;
        private bool _previousNewCalculation;
        private bool _previousOperatorPressed;

        public FunctionCommand(CalculatorModel model, string functionType)
        {
            _model = model;
            _functionType = functionType;
        }

        public void Execute()
        {
            // Сохраняем состояние
            _previousInput = _model.CurrentInput;
            _previousOperation = _model.CurrentOperation;
            _previousFirstNumber = _model.FirstNumber;
            _previousNewCalculation = _model.IsNewCalculation;
            _previousOperatorPressed = _model.IsOperatorJustPressed;

            // Выполняем команду
            switch (_functionType)
            {
                case "Clear":
                    _model.Clear();
                    break;
                case "Delete":
                    _model.DeleteLastDigit();
                    break;
                case "PlusMinus":
                    _model.ToggleSign();
                    break;
                case "Equals":
                    _model.CalculateResult();
                    break;
                case "Decimal":
                    _model.AddDecimalPoint();
                    break;
            }
        }

        public void Undo()
        {
            _model.CurrentInput = _previousInput;
            _model.CurrentOperation = _previousOperation;
            _model.FirstNumber = _previousFirstNumber;
            _model.IsNewCalculation = _previousNewCalculation;
            _model.IsOperatorJustPressed = _previousOperatorPressed;
        }

        public string Description
        {
            get
            {
                switch (_functionType)
                {
                    case "Clear": return "Очистка";
                    case "Delete": return "Удаление символа";
                    case "PlusMinus": return "Смена знака";
                    case "Equals": return "Вычисление";
                    case "Decimal": return "Десятичная точка";
                    default: return _functionType;
                }
            }
        }
    }
}