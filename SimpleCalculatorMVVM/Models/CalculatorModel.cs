using System;

namespace SimpleCalculatorMVVM.Models
{
    public class CalculatorModel
    {
        private string _currentInput = "0";
        private string _currentOperation = "";
        private double _firstNumber = 0;
        private bool _isNewCalculation = true;
        private bool _isOperatorJustPressed = false;
        private double _memoryValue = 0;

        public string CurrentInput
        {
            get => _currentInput;
            set => _currentInput = value;
        }

        public string CurrentOperation
        {
            get => _currentOperation;
            set => _currentOperation = value;
        }

        public double FirstNumber
        {
            get => _firstNumber;
            set => _firstNumber = value;
        }

        public bool IsNewCalculation
        {
            get => _isNewCalculation;
            set => _isNewCalculation = value;
        }

        public bool IsOperatorJustPressed
        {
            get => _isOperatorJustPressed;
            set => _isOperatorJustPressed = value;
        }

        public double MemoryValue
        {
            get => _memoryValue;
            set => _memoryValue = value;
        }

        public void CalculateResult()
        {
            try
            {
                double secondNumber = double.Parse(_currentInput);
                double result = 0;

                switch (_currentOperation)
                {
                    case "+":
                        result = _firstNumber + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - secondNumber;
                        break;
                    case "x":
                        result = _firstNumber * secondNumber;
                        break;
                    case "÷":
                        if (secondNumber == 0)
                            throw new DivideByZeroException();
                        result = _firstNumber / secondNumber;
                        break;
                    case "^":
                        result = Math.Pow(_firstNumber, secondNumber);
                        break;
                }

                _currentInput = result.ToString();
                _firstNumber = result;
                _isNewCalculation = true;
            }
            catch (DivideByZeroException)
            {
                _currentInput = "Ошибка: деление на ноль";
            }
            catch (Exception)
            {
                _currentInput = "Ошибка";
                _currentOperation = "";
                _isNewCalculation = true;
            }
        }

        public void Reset()
        {
            _currentInput = "0";
            _currentOperation = "";
            _firstNumber = 0;
            _isNewCalculation = true;
            _isOperatorJustPressed = false;
        }
    }
}