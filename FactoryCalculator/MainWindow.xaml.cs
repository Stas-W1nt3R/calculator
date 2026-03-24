using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace FactoryCalculator
{
    public partial class MainWindow : Window
    {
        private string _currentInput = "0";
        private string _currentOperation = "";
        private double _firstNumber = 0;
        private bool _isNewCalculation = true;
        private bool _isOperatorJustPressed = false;
        private IButtonFactory _buttonFactory;

        // Публичные свойства для доступа из классов кнопок
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
       

        public MainWindow()
        {
            InitializeComponent();

            // Сохраняем ссылку на TextBox
            DisplayTextBox = this.FindName("DisplayTextBox") as TextBox;

            _buttonFactory = new CalculatorButtonFactory();
            DisplayTextBox.Text = _currentInput;

            // Подключаем обработчик клавиатуры
            this.KeyDown += MainWindow_KeyDown;
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
                        {
                            MessageBox.Show("Нельзя делить на ноль", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        result = _firstNumber / secondNumber;
                        break;
                }

                _currentInput = result.ToString();
                DisplayTextBox.Text = _currentInput;
                _firstNumber = result;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Ошибка";
                _currentInput = "0";
                _currentOperation = "";
                _isNewCalculation = true;
            }
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();
            IButton digitButton = _buttonFactory.CreateDigitButton(digit);
            digitButton.Execute(this);
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();
            IButton operatorButton = _buttonFactory.CreateOperatorButton(operation);
            operatorButton.Execute(this);
        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();
            string functionType = "";

            switch (content)
            {
                case "C": functionType = "Clear"; break;
                case "⌫": functionType = "Delete"; break;
                case "±": functionType = "PlusMinus"; break;
                case "=": functionType = "Equals"; break;
                case ".": functionType = "Decimal"; break;
            }

            IButton functionButton = _buttonFactory.CreateFunctionButton(content, functionType);
            functionButton.Execute(this);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Цифры (верхний ряд)
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = e.Key.ToString().Last().ToString();
                IButton button = _buttonFactory.CreateDigitButton(digit);
                button.Execute(this);
            }
            // Цифры (NumPad)
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                IButton button = _buttonFactory.CreateDigitButton(digit);
                button.Execute(this);
            }
            // Сложение
            else if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                IButton button = _buttonFactory.CreateOperatorButton("+");
                button.Execute(this);
            }
            // Вычитание
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                IButton button = _buttonFactory.CreateOperatorButton("-");
                button.Execute(this);
            }
            // Умножение
            else if (e.Key == Key.Multiply)
            {
                IButton button = _buttonFactory.CreateOperatorButton("x");
                button.Execute(this);
            }
            // Деление
            else if (e.Key == Key.Divide || e.Key == Key.OemQuestion)
            {
                IButton button = _buttonFactory.CreateOperatorButton("÷");
                button.Execute(this);
            }
            // Равно (Enter)
            else if (e.Key == Key.Enter)
            {
                IButton button = _buttonFactory.CreateFunctionButton("=", "Equals");
                button.Execute(this);
            }
            // Десятичная точка
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod || e.Key == Key.OemComma)
            {
                IButton button = _buttonFactory.CreateFunctionButton(".", "Decimal");
                button.Execute(this);
            }
            // Очистка (Escape)
            else if (e.Key == Key.Escape)
            {
                IButton button = _buttonFactory.CreateFunctionButton("C", "Clear");
                button.Execute(this);
            }
            // Удаление (Backspace)
            else if (e.Key == Key.Back)
            {
                IButton button = _buttonFactory.CreateFunctionButton("⌫", "Delete");
                button.Execute(this);
            }
        }
    }
}