using SimpleCalculatorMVVM.CommandManager;
using SimpleCalculatorMVVM.Commands;
using SimpleCalculatorMVVM.Decorators;
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
        private readonly CommandInvoker _invoker;
        private bool _isScientificPopupOpen;

        public MainViewModel()
        {
            CalculatorModel baseModel = new CalculatorModel();           
            baseModel = new ValidationDecorator(baseModel);             
            baseModel = new RoundingDecorator(baseModel, 10);          
            _model = baseModel;


            _invoker = new CommandInvoker();

            // Создаем команды-обертки для UI
            DigitCommand = new RelayCommand(ExecuteDigit);
            OperatorCommand = new RelayCommand(ExecuteOperator);
            FunctionCommand = new RelayCommand(ExecuteFunction);
            ScientificCommand = new RelayCommand(ExecuteScientific);
            ToggleScientificCommand = new RelayCommand(_ => ToggleScientificPopup());
            UndoCommand = new RelayCommand(_ => ExecuteUndo(), _ => _invoker.CanUndo);
            RedoCommand = new RelayCommand(_ => ExecuteRedo(), _ => _invoker.CanRedo);
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

        // Свойства для Undo/Redo
        public bool CanUndo => _invoker.CanUndo;
        public bool CanRedo => _invoker.CanRedo;
        public string UndoDescription => _invoker.GetUndoDescription();
        public string RedoDescription => _invoker.GetRedoDescription();

        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand FunctionCommand { get; }
        public ICommand ScientificCommand { get; }
        public ICommand ToggleScientificCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        private void ExecuteDigit(object parameter)
        {
            string digit = parameter?.ToString() ?? "0";
            var command = new DigitCommand(_model, digit);
            _invoker.ExecuteCommand(command);
            UpdateDisplay();
            UpdateCommandState();
        }

        private void ExecuteOperator(object parameter)
        {
            string operation = parameter?.ToString() ?? "";
            var command = new OperatorCommand(_model, operation);
            _invoker.ExecuteCommand(command);
            UpdateDisplay();
            UpdateCommandState();
        }

        private void ExecuteFunction(object parameter)
        {
            string function = parameter?.ToString() ?? "";
            var command = new FunctionCommand(_model, function);
            _invoker.ExecuteCommand(command);
            UpdateDisplay();
            UpdateCommandState();
        }

        private void ExecuteScientific(object parameter)
        {
            string function = parameter?.ToString() ?? "";
            var command = new ScientificCommand(_model, function);
            _invoker.ExecuteCommand(command);
            UpdateDisplay();
            UpdateCommandState();
        }

        private void ExecuteUndo()
        {
            _invoker.Undo();
            UpdateDisplay();
            UpdateCommandState();
        }

        private void ExecuteRedo()
        {
            _invoker.Redo();
            UpdateDisplay();
            UpdateCommandState();
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged(nameof(DisplayText));
        }

        private void UpdateCommandState()
        {
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(UndoDescription));
            OnPropertyChanged(nameof(RedoDescription));
            ((RelayCommand)UndoCommand).RaiseCanExecuteChanged();
            ((RelayCommand)RedoCommand).RaiseCanExecuteChanged();
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