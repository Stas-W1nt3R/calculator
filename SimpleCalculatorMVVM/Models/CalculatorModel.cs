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

        // ЛОГИКА ДЛЯ ЦИФР
        public void AddDigit(string digit)
        {
            // Если начинаем новое вычисление или только что нажали оператор
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

        // ЛОГИКА ДЛЯ ОПЕРАТОРОВ
        public void SetOperator(string operation)
        {
            if (!_isOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(_currentOperation))
                {
                    // Если уже есть операция, вычисляем результат
                    CalculateResult();
                    _firstNumber = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                }
                else
                {
                    // Сохраняем первое число
                    _firstNumber = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                }
            }

            _currentOperation = operation;
            _isOperatorJustPressed = true;
            _isNewCalculation = true; // После нажатия оператора начинаем новый ввод
        }

        // ЛОГИКА ВЫЧИСЛЕНИЙ
        public void CalculateResult()
        {
            try
            {
                double secondNumber = double.Parse(_currentInput, CultureInfo.InvariantCulture);
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
                    case "*":
                        result = _firstNumber * secondNumber;
                        break;
                    case "÷":
                    case "/":
                        if (secondNumber == 0)
                            throw new DivideByZeroException();
                        result = _firstNumber / secondNumber;
                        break;
                    case "^":
                        result = Math.Pow(_firstNumber, secondNumber);
                        break;
                    default:
                        return;
                }

                // Форматируем результат (убираем лишние знаки после запятой)
                _currentInput = FormatResult(result);
                _firstNumber = result;
                _currentOperation = "";
                _isNewCalculation = true;
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

        // Форматирование результата
        private string FormatResult(double result)
        {
            // Если число целое, показываем без десятичной части
            if (result == Math.Floor(result))
                return result.ToString("0", CultureInfo.InvariantCulture);

            // Иначе показываем с десятичной точкой, но ограничиваем 10 знаками
            return result.ToString("0.##########", CultureInfo.InvariantCulture);
        }

        // ЛОГИКА ФУНКЦИЙ
        public void Clear()
        {
            _currentInput = "0";
            _currentOperation = "";
            _firstNumber = 0;
            _isNewCalculation = true;
            _isOperatorJustPressed = false;
        }

        public void DeleteLastDigit()
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

        public void ToggleSign()
        {
            if (!_isOperatorJustPressed && !_isNewCalculation && _currentInput != "0")
            {
                if (_currentInput.StartsWith("-"))
                    _currentInput = _currentInput.Substring(1);
                else
                    _currentInput = "-" + _currentInput;
            }
        }

        public void AddDecimalPoint()
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

        // НАУЧНЫЕ ФУНКЦИИ
        public void SquareRoot()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                if (number < 0)
                {
                    _currentInput = "Ошибка";
                    Reset();
                    return;
                }
                _currentInput = FormatResult(Math.Sqrt(number));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Square()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentInput = FormatResult(Math.Pow(number, 2));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void PowerY()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentOperation = "^";
                _firstNumber = number;
                _isOperatorJustPressed = true;
                _isNewCalculation = true;
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Log10()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                if (number <= 0)
                {
                    _currentInput = "Ошибка";
                    Reset();
                    return;
                }
                _currentInput = FormatResult(Math.Log10(number));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Ln()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                if (number <= 0)
                {
                    _currentInput = "Ошибка";
                    Reset();
                    return;
                }
                _currentInput = FormatResult(Math.Log(number));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Sin()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentInput = FormatResult(Math.Sin(number * Math.PI / 180));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Cos()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentInput = FormatResult(Math.Cos(number * Math.PI / 180));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Tan()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentInput = FormatResult(Math.Tan(number * Math.PI / 180));
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        public void Percent()
        {
            try
            {
                double number = double.Parse(_currentInput, CultureInfo.InvariantCulture);
                _currentInput = FormatResult(number / 100);
                _isNewCalculation = true;
                _currentOperation = "";
            }
            catch
            {
                _currentInput = "Ошибка";
                Reset();
            }
        }

        // ПАМЯТЬ
        public void MemorySave()
        {
            try
            {
                _memoryValue = double.Parse(_currentInput, CultureInfo.InvariantCulture);
            }
            catch
            {
                _memoryValue = 0;
            }
        }

        public void MemoryRecall()
        {
            _currentInput = FormatResult(_memoryValue);
            _isNewCalculation = true;
            _isOperatorJustPressed = false;
        }

        public void MemoryClear()
        {
            _memoryValue = 0;
        }

        public void MemoryAdd()
        {
            try
            {
                _memoryValue += double.Parse(_currentInput, CultureInfo.InvariantCulture);
            }
            catch
            {
                _memoryValue += 0;
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