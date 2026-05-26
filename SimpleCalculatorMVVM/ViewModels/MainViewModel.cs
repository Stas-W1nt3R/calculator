using Library;
using SimpleCalculatorMVVM.CommandManager;
using SimpleCalculatorMVVM.Commands;
using SimpleCalculatorMVVM.Decorators;
using SimpleCalculatorMVVM.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SimpleCalculatorMVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _model;
        private readonly CommandInvoker _invoker;
        private bool _isScientificPopupOpen;

        // ← НОВОЕ: событие для смены темы
        public event EventHandler<string> ThemeChanged;

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

            ShowAboutCommand = new RelayCommand(_ => ShowAbout());
            ShowHelpCommand = new RelayCommand(_ => ShowHelp());

            // ← НОВАЯ КОМАНДА: смена темы
            ChangeThemeCommand = new RelayCommand(ExecuteChangeTheme);
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

        public ICommand ShowAboutCommand { get; }
        public ICommand ShowHelpCommand { get; }

        // ← НОВОЕ: команда смены темы
        public ICommand ChangeThemeCommand { get; }

        private void ShowAbout()
        {
            try
            {
                var aboutWindow = new AboutWindow();
                aboutWindow.Owner = Application.Current.MainWindow;
                aboutWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия окна: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowHelp()
        {
            try
            {
                var helpWindow = new HelpWindow();
                helpWindow.Owner = Application.Current.MainWindow;
                helpWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия окна: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ← НОВЫЙ МЕТОД: смена темы
        private void ExecuteChangeTheme(object parameter)
        {
            string theme = parameter?.ToString() ?? "Light";

            AppConfig.Current.Theme = theme;
            AppConfig.Save();

            // Уведомляем View о смене темы
            ThemeChanged?.Invoke(this, theme);
        }

        private void ExecuteDigit(object parameter)
        {
            string digit = parameter?.ToString() ?? "0";

            SoundManager.PlayClick();

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