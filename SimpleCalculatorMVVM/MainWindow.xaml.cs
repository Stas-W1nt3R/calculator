using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SimpleCalculatorMVVM.ViewModels;

namespace SimpleCalculatorMVVM
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Если открыто окно научных функций
            if (_viewModel.IsScientificPopupOpen)
            {
                if (e.Key == Key.Escape)
                {
                    _viewModel.ToggleScientificCommand.Execute(null);
                    e.Handled = true;
                }
                return;
            }

            // Ctrl+Z для Undo
            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_viewModel.CanUndo)
                    _viewModel.UndoCommand.Execute(null);
                e.Handled = true;
                return;
            }

            // Ctrl+Y для Redo
            if (e.Key == Key.Y && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_viewModel.CanRedo)
                    _viewModel.RedoCommand.Execute(null);
                e.Handled = true;
                return;
            }

            // Цифры
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = e.Key.ToString().Last().ToString();
                _viewModel.DigitCommand.Execute(digit);
                e.Handled = true;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                _viewModel.DigitCommand.Execute(digit);
                e.Handled = true;
            }
            // Операторы
            else if (e.Key == Key.Add)
            {
                _viewModel.OperatorCommand.Execute("+");
                e.Handled = true;
            }
            else if (e.Key == Key.Subtract)
            {
                _viewModel.OperatorCommand.Execute("-");
                e.Handled = true;
            }
            else if (e.Key == Key.Multiply)
            {
                _viewModel.OperatorCommand.Execute("x");
                e.Handled = true;
            }
            else if (e.Key == Key.Divide)
            {
                _viewModel.OperatorCommand.Execute("÷");
                e.Handled = true;
            }
            // Функции
            else if (e.Key == Key.Enter)
            {
                _viewModel.FunctionCommand.Execute("Equals");
                e.Handled = true;
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                _viewModel.FunctionCommand.Execute("Decimal");
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                _viewModel.FunctionCommand.Execute("Clear");
                e.Handled = true;
            }
            else if (e.Key == Key.Back)
            {
                _viewModel.FunctionCommand.Execute("Delete");
                e.Handled = true;
            }
        }
    }
}