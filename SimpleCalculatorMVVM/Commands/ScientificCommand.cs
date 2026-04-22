using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Commands
{
    public class ScientificCommand : ICalculatorCommand
    {
        private readonly CalculatorModel _model;
        private readonly string _functionType;
        private string _previousInput;
        private string _previousOperation;
        private double _previousFirstNumber;
        private bool _previousNewCalculation;
        private bool _previousOperatorPressed;

        public ScientificCommand(CalculatorModel model, string functionType)
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
                case "Sqrt":
                    _model.SquareRoot();
                    break;
                case "Power":
                    _model.Square();
                    break;
                case "PowerY":
                    _model.PowerY();
                    break;
                case "Log":
                    _model.Log10();
                    break;
                case "Ln":
                    _model.Ln();
                    break;
                case "Sin":
                    _model.Sin();
                    break;
                case "Cos":
                    _model.Cos();
                    break;
                case "Tan":
                    _model.Tan();
                    break;
                case "Percent":
                    _model.Percent();
                    break;
                case "MemorySave":
                    _model.MemorySave();
                    break;
                case "MemoryRecall":
                    _model.MemoryRecall();
                    break;
                case "MemoryClear":
                    _model.MemoryClear();
                    break;
                case "MemoryAdd":
                    _model.MemoryAdd();
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
                    case "Sqrt": return "Квадратный корень";
                    case "Power": return "Квадрат числа";
                    case "PowerY": return "Возведение в степень";
                    case "Log": return "Логарифм по основанию 10";
                    case "Ln": return "Натуральный логарифм";
                    case "Sin": return "Синус";
                    case "Cos": return "Косинус";
                    case "Tan": return "Тангенс";
                    case "Percent": return "Процент";
                    case "MemorySave": return "Сохранить в память";
                    case "MemoryRecall": return "Восстановить из памяти";
                    case "MemoryClear": return "Очистить память";
                    case "MemoryAdd": return "Добавить в память";
                    default: return _functionType;
                }
            }
        }
    }
}