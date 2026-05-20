using SimpleCalculatorMVVM.Models;
using System;

namespace SimpleCalculatorMVVM.Decorators
{
    public class RoundingDecorator : CalculatorDecorator
    {
        private int _decimalPlaces = 10;

        public RoundingDecorator(CalculatorModel calculator, int decimalPlaces = 10) : base(calculator)
        {
            _decimalPlaces = decimalPlaces;
        }

        private string RoundResult(string input)
        {
            if (double.TryParse(input, out double value))
            {
                double rounded = Math.Round(value, _decimalPlaces, MidpointRounding.AwayFromZero);
                return rounded.ToString();
            }
            return input;
        }

        public override void CalculateResult()
        {
            _calculator.CalculateResult();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void SquareRoot()
        {
            _calculator.SquareRoot();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void Square()
        {
            _calculator.Square();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void Sin()
        {
            _calculator.Sin();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void Cos()
        {
            _calculator.Cos();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void Tan()
        {
            _calculator.Tan();
            CurrentInput = RoundResult(CurrentInput);
        }

        public override void Percent()
        {
            _calculator.Percent();
            CurrentInput = RoundResult(CurrentInput);
        }
    }
}