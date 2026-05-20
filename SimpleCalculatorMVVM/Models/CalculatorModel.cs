using System;
using System.Globalization;

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

        public virtual string CurrentInput
        {
            get => _currentInput;
            set => _currentInput = value;
        }

        public virtual string CurrentOperation
        {
            get => _currentOperation;
            set => _currentOperation = value;
        }

        public virtual double FirstNumber
        {
            get => _firstNumber;
            set => _firstNumber = value;
        }

        public virtual bool IsNewCalculation
        {
            get => _isNewCalculation;
            set => _isNewCalculation = value;
        }

        public virtual bool IsOperatorJustPressed
        {
            get => _isOperatorJustPressed;
            set => _isOperatorJustPressed = value;
        }

        public virtual double MemoryValue
        {
            get => _memoryValue;
            set => _memoryValue = value;
        }

        public virtual void AddDigit(string digit)
        {
            if (_isNewCalculation || _currentInput == "0" || _isOperatorJustPressed)
            {
                _currentInput = digit;
                _isNewCalculation = false;
                _isOperatorJustPressed = false;
            }
            else
            {
                _currentInput += digit;
            }
        }

        public virtual void SetOperator(string operation)
        {
            if (!_isOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(_currentOperation))
                {
                    CalculateResult();
                }
                else
                {
                    _firstNumber = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                }
            }

            _currentOperation = operation;
            _isOperatorJustPressed = true;
            _isNewCalculation = true;
        }

        public virtual void CalculateResult()
        {
            try
            {
                double secondNumber = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                double result = 0;

                switch (_currentOperation)
                {
                    case "+": result = _firstNumber + secondNumber; break;
                    case "-": result = _firstNumber - secondNumber; break;
                    case "x": result = _firstNumber * secondNumber; break;
                    case "÷":
                        if (secondNumber == 0) throw new DivideByZeroException();
                        result = _firstNumber / secondNumber;
                        break;
                    case "^": result = Math.Pow(_firstNumber, secondNumber); break;
                }

                _currentInput = result.ToString(CultureInfo.InvariantCulture);
                _firstNumber = result;
                _isNewCalculation = true;
                _currentOperation = "";
                _isOperatorJustPressed = false;
            }
            catch (DivideByZeroException)
            {
                _currentInput = "Ошибка: деление на ноль";
                Reset();
            }
            catch (Exception)
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public virtual void Clear()
        {
            _currentInput = "0";
            _currentOperation = "";
            _firstNumber = 0;
            _isNewCalculation = true;
            _isOperatorJustPressed = false;
        }

        public virtual void DeleteLastDigit()
        {
            if (!_isOperatorJustPressed && _currentInput.Length > 0 && !_isNewCalculation)
            {
                if (_currentInput.Length > 1)
                    _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                else
                    _currentInput = "0";

                if (string.IsNullOrEmpty(_currentInput) || _currentInput == "-")
                    _currentInput = "0";
            }
        }

        public virtual void ToggleSign()
        {
            if (!_isOperatorJustPressed && !_isNewCalculation && _currentInput != "0")
            {
                if (_currentInput.StartsWith("-"))
                    _currentInput = _currentInput.Substring(1);
                else
                    _currentInput = "-" + _currentInput;
            }
        }

        public virtual void AddDecimalPoint()
        {
            if (_isOperatorJustPressed)
            {
                _currentInput = "0.";
                _isOperatorJustPressed = false;
                _isNewCalculation = false;
            }
            else if (!_currentInput.Contains("."))
            {
                _currentInput += ".";
                _isNewCalculation = false;
            }
        }

        public virtual void SquareRoot()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            if (number < 0)
            {
                _currentInput = "Ошибка";
                Reset();
                return;
            }
            _currentInput = Math.Sqrt(number).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Square()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentInput = Math.Pow(number, 2).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void PowerY()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentOperation = "^";
            _firstNumber = number;
            _isOperatorJustPressed = true;
            _isNewCalculation = true;
        }

        public virtual void Sin()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentInput = Math.Sin(number * Math.PI / 180).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Cos()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentInput = Math.Cos(number * Math.PI / 180).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Tan()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentInput = Math.Tan(number * Math.PI / 180).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Percent()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            _currentInput = (number / 100).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Log10()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            if (number <= 0)
            {
                _currentInput = "Ошибка";
                Reset();
                return;
            }
            _currentInput = Math.Log10(number).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void Ln()
        {
            double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            if (number <= 0)
            {
                _currentInput = "Ошибка";
                Reset();
                return;
            }
            _currentInput = Math.Log(number).ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void MemorySave()
        {
            _memoryValue = double.Parse(_currentInput, CultureInfo.InvariantCulture);
        }

        public virtual void MemoryRecall()
        {
            _currentInput = _memoryValue.ToString(CultureInfo.InvariantCulture);
            _isNewCalculation = true;
        }

        public virtual void MemoryClear()
        {
            _memoryValue = 0;
        }

        public virtual void MemoryAdd()
        {
            _memoryValue += double.Parse(_currentInput, CultureInfo.InvariantCulture);
        }

        public virtual void Reset()
        {
            Clear();
        }
    }
}