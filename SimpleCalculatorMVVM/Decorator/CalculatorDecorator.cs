using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Decorators
{
    public abstract class CalculatorDecorator : CalculatorModel
    {
        protected CalculatorModel _calculator;

        public CalculatorDecorator(CalculatorModel calculator)
        {
            _calculator = calculator;
        }

        public override string CurrentInput
        {
            get => _calculator.CurrentInput;
            set => _calculator.CurrentInput = value;
        }

        public override string CurrentOperation
        {
            get => _calculator.CurrentOperation;
            set => _calculator.CurrentOperation = value;
        }

        public override double FirstNumber
        {
            get => _calculator.FirstNumber;
            set => _calculator.FirstNumber = value;
        }

        public override bool IsNewCalculation
        {
            get => _calculator.IsNewCalculation;
            set => _calculator.IsNewCalculation = value;
        }

        public override bool IsOperatorJustPressed
        {
            get => _calculator.IsOperatorJustPressed;
            set => _calculator.IsOperatorJustPressed = value;
        }

        public override double MemoryValue
        {
            get => _calculator.MemoryValue;
            set => _calculator.MemoryValue = value;
        }

        public override void AddDigit(string digit) => _calculator.AddDigit(digit);
        public override void SetOperator(string operation) => _calculator.SetOperator(operation);
        public override void CalculateResult() => _calculator.CalculateResult();
        public override void Clear() => _calculator.Clear();
        public override void DeleteLastDigit() => _calculator.DeleteLastDigit();
        public override void ToggleSign() => _calculator.ToggleSign();
        public override void AddDecimalPoint() => _calculator.AddDecimalPoint();
        public override void SquareRoot() => _calculator.SquareRoot();
        public override void Square() => _calculator.Square();
        public override void PowerY() => _calculator.PowerY();
        public override void Sin() => _calculator.Sin();
        public override void Cos() => _calculator.Cos();
        public override void Tan() => _calculator.Tan();
        public override void Percent() => _calculator.Percent();
        public override void MemorySave() => _calculator.MemorySave();
        public override void MemoryRecall() => _calculator.MemoryRecall();
        public override void MemoryClear() => _calculator.MemoryClear();
        public override void MemoryAdd() => _calculator.MemoryAdd();
        public override void Reset() => _calculator.Reset();
    }
}