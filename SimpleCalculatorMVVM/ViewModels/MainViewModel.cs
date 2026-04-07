using SimpleCalculatorMVVM.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleCalculatorMVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _model;
        private bool _isScientificPopupOpen;

        public MainViewModel()
        {
            _model = new CalculatorModel();

            DigitCommand = new RelayCommand(ExecuteDigit);
            OperatorCommand = new RelayCommand(ExecuteOperator);
            FunctionCommand = new RelayCommand(ExecuteFunction);
            ScientificCommand = new RelayCommand(ExecuteScientific);
            ToggleScientificCommand = new RelayCommand(_ => ToggleScientificPopup());
        }

        public string DisplayText
        {
            get => _model.CurrentInput;
            set
            {
                _model.CurrentInput = value;
                OnPropertyChanged();
            }
        }

        public bool IsScientificPopupOpen
        {
            get => _isScientificPopupOpen;
            set
            {
                _isScientificPopupOpen = value;
                OnPropertyChanged();
            }
        }

        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand FunctionCommand { get; }
        public ICommand ScientificCommand { get; }
        public ICommand ToggleScientificCommand { get; }

        private void ExecuteDigit(object parameter)
        {
            string digit = parameter?.ToString() ?? "0";

            if (_model.IsNewCalculation || _model.CurrentInput == "0" || _model.IsOperatorJustPressed)
            {
                DisplayText = digit;
                _model.IsNewCalculation = false;
                _model.IsOperatorJustPressed = false;
            }
            else
            {
                DisplayText += digit;
            }
        }

        private void ExecuteOperator(object parameter)
        {
            string operation = parameter?.ToString() ?? "";

            if (!_model.IsOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(_model.CurrentOperation))
                {
                    _model.CalculateResult();
                    DisplayText = _model.CurrentInput;
                }
                else
                {
                    if (double.TryParse(_model.CurrentInput, out double number))
                        _model.FirstNumber = number;
                    else
                        _model.FirstNumber = 0;
                }
            }

            _model.CurrentOperation = operation;
            _model.IsOperatorJustPressed = true;
        }

        private void ExecuteFunction(object parameter)
        {
            string function = parameter?.ToString() ?? "";

            switch (function)
            {
                case "Clear":
                    _model.Reset();
                    DisplayText = _model.CurrentInput;
                    break;

                case "Delete":
                    if (!_model.IsOperatorJustPressed &&
                        _model.CurrentInput.Length > 0 &&
                        !_model.IsNewCalculation)
                    {
                        if (_model.CurrentInput.Length > 1)
                            DisplayText = _model.CurrentInput.Substring(0, _model.CurrentInput.Length - 1);
                        else
                            DisplayText = "0";

                        if (string.IsNullOrEmpty(DisplayText) || DisplayText == "-")
                            DisplayText = "0";
                    }
                    break;

                case "PlusMinus":
                    if (!_model.IsOperatorJustPressed &&
                        !_model.IsNewCalculation &&
                        _model.CurrentInput != "0")
                    {
                        DisplayText = _model.CurrentInput.StartsWith("-")
                            ? _model.CurrentInput.Substring(1)
                            : "-" + _model.CurrentInput;
                    }
                    break;

                case "Equals":
                    if (!string.IsNullOrEmpty(_model.CurrentOperation) &&
                        !_model.IsOperatorJustPressed)
                    {
                        _model.CalculateResult();
                        DisplayText = _model.CurrentInput;
                        _model.CurrentOperation = "";
                        _model.IsNewCalculation = true;
                    }
                    break;

                case "Decimal":
                    if (_model.IsOperatorJustPressed)
                    {
                        DisplayText = "0,";
                        _model.IsOperatorJustPressed = false;
                    }
                    else if (!_model.CurrentInput.Contains(","))
                    {
                        _model.IsNewCalculation = false;
                        DisplayText += ",";
                    }
                    break;
            }
        }

        private void ExecuteScientific(object parameter)
        {
            string function = parameter?.ToString() ?? "";

            if (!double.TryParse(_model.CurrentInput, out double currentNumber))
            {
                DisplayText = "Ошибка";
                return;
            }

            double result = 0;

            switch (function)
            {
                case "Sqrt":
                    if (currentNumber < 0)
                    {
                        DisplayText = "Ошибка";
                        return;
                    }
                    result = Math.Sqrt(currentNumber);
                    break;

                case "Power":
                    result = Math.Pow(currentNumber, 2);
                    break;

                case "PowerY":
                    _model.CurrentOperation = "^";
                    _model.FirstNumber = currentNumber;
                    _model.IsOperatorJustPressed = true;
                    return;

                case "Log":
                    if (currentNumber <= 0)
                    {
                        DisplayText = "Ошибка";
                        return;
                    }
                    result = Math.Log10(currentNumber);
                    break;

                case "Ln":
                    if (currentNumber <= 0)
                    {
                        DisplayText = "Ошибка";
                        return;
                    }
                    result = Math.Log(currentNumber);
                    break;

                case "Sin":
                    result = Math.Sin(currentNumber * Math.PI / 180);
                    break;

                case "Cos":
                    result = Math.Cos(currentNumber * Math.PI / 180);
                    break;

                case "Tan":
                    result = Math.Tan(currentNumber * Math.PI / 180);
                    break;

                case "Percent":
                    result = currentNumber / 100;
                    break;

                case "MemorySave":
                    _model.MemoryValue = currentNumber;
                    return;

                case "MemoryRecall":
                    DisplayText = _model.MemoryValue.ToString();
                    return;

                case "MemoryClear":
                    _model.MemoryValue = 0;
                    return;

                case "MemoryAdd":
                    _model.MemoryValue += currentNumber;
                    return;

                default:
                    return;
            }

            DisplayText = result.ToString();
            _model.IsNewCalculation = true;
        }

        private void ToggleScientificPopup()
        {
            IsScientificPopupOpen = !IsScientificPopupOpen;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}