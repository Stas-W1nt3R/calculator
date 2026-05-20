using SimpleCalculatorMVVM.Models;

namespace SimpleCalculatorMVVM.Decorators
{
    public class ValidationDecorator : CalculatorDecorator
    {
        public ValidationDecorator(CalculatorModel calculator) : base(calculator)
        {
        }

        public override void CalculateResult()
        {
            // Защита от деления на ноль
            if (CurrentOperation == "÷" && CurrentInput == "0")
            {
                CurrentInput = "Ошибка: деление на ноль";
                Reset();
                return;
            }

            _calculator.CalculateResult();
        }

        public override void AddDecimalPoint()
        {
            // Запрещаем больше одной точки
            if (CurrentInput.Contains("."))
                return;

            _calculator.AddDecimalPoint();
        }
    }
}