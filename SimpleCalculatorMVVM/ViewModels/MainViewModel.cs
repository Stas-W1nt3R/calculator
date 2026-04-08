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
            _model.AddDigit(digit);
            UpdateDisplay();
        }

        private void ExecuteOperator(object parameter)
        {
            string operation = parameter?.ToString() ?? "";
            _model.SetOperator(operation);
            UpdateDisplay();
        }

        private void ExecuteFunction(object parameter)
        {
            string function = parameter?.ToString() ?? "";

            switch (function)
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
            UpdateDisplay();
        }

        private void ExecuteScientific(object parameter)
        {
            string function = parameter?.ToString() ?? "";

            switch (function)
            {
                case "Sqrt":
                    _model.SquareRoot();
                    break;
                case "Power":
                    _model.Square();
                    break;
                case "PowerY":
                    _model.PowerY();
                    UpdateDisplay();
                    return;
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
                    UpdateDisplay();
                    return;
                case "MemoryRecall":
                    _model.MemoryRecall();
                    break;
                case "MemoryClear":
                    _model.MemoryClear();
                    UpdateDisplay();
                    return;
                case "MemoryAdd":
                    _model.MemoryAdd();
                    UpdateDisplay();
                    return;
            }
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged(nameof(DisplayText));
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