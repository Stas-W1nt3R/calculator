using SimpleCalculatorMVVM.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleCalculatorMVVM
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
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

            if (_viewModel.IsScientificPopupOpen)
                _viewModel.ToggleScientificCommand.Execute(null);
        }
    }
}