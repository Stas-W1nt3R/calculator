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
        private double _memoryValue = 0;
        private IButtonFactory _buttonFactory;

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

        public MainWindow()
        {
            InitializeComponent();
            _buttonFactory = new CalculatorButtonFactory();
            DisplayTextBox.Text = _currentInput;
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
                    case "^":
                        result = Math.Pow(_firstNumber, secondNumber);
                        break;
                }

                _currentInput = result.ToString();
                DisplayTextBox.Text = _currentInput;
                _firstNumber = result;
                _isNewCalculation = true;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Ошибка";
                _currentInput = "0";
                _currentOperation = "";
                _isNewCalculation = true;
            }
        }

        private void ToggleScientificPanel_Click(object sender, RoutedEventArgs e)
        {
            // Открываем или закрываем Popup
            if (ScientificPopup.IsOpen)
                ScientificPopup.IsOpen = false;
            else
                ScientificPopup.IsOpen = true;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();
            IButton digitButton = _buttonFactory.CreateDigitButton(digit);
            digitButton.Execute(this);
            // Закрываем Popup если открыт
            if (ScientificPopup.IsOpen) ScientificPopup.IsOpen = false;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();
            IButton operatorButton = _buttonFactory.CreateOperatorButton(operation);
            operatorButton.Execute(this);
            // Закрываем Popup если открыт
            if (ScientificPopup.IsOpen) ScientificPopup.IsOpen = false;
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
            // Закрываем Popup если открыт
            if (ScientificPopup.IsOpen) ScientificPopup.IsOpen = false;
        }

        private void ScientificButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();
            string functionType = "";

            switch (content)
            {
                case "√": functionType = "Sqrt"; break;
                case "x²": functionType = "Power"; break;
                case "xʸ": functionType = "PowerY"; break;
                case "log": functionType = "Log"; break;
                case "ln": functionType = "Ln"; break;
                case "sin": functionType = "Sin"; break;
                case "cos": functionType = "Cos"; break;
                case "tan": functionType = "Tan"; break;
                case "%": functionType = "Percent"; break;
                case "MS": functionType = "MemorySave"; break;
                case "MR": functionType = "MemoryRecall"; break;
                case "MC": functionType = "MemoryClear"; break;
                case "M+": functionType = "MemoryAdd"; break;
            }

            IButton sciButton = _buttonFactory.CreateScientificButton(content, functionType);
            sciButton.Execute(this);

            // Закрываем Popup после нажатия научной кнопки
            if (ScientificPopup.IsOpen) ScientificPopup.IsOpen = false;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = e.Key.ToString().Last().ToString();
                IButton button = _buttonFactory.CreateDigitButton(digit);
                button.Execute(this);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                IButton button = _buttonFactory.CreateDigitButton(digit);
                button.Execute(this);
            }
            else if (e.Key == Key.Add)
            {
                IButton button = _buttonFactory.CreateOperatorButton("+");
                button.Execute(this);
            }
            else if (e.Key == Key.Subtract)
            {
                IButton button = _buttonFactory.CreateOperatorButton("-");
                button.Execute(this);
            }
            else if (e.Key == Key.Multiply)
            {
                IButton button = _buttonFactory.CreateOperatorButton("x");
                button.Execute(this);
            }
            else if (e.Key == Key.Divide)
            {
                IButton button = _buttonFactory.CreateOperatorButton("÷");
                button.Execute(this);
            }
            else if (e.Key == Key.Enter)
            {
                IButton button = _buttonFactory.CreateFunctionButton("=", "Equals");
                button.Execute(this);
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                IButton button = _buttonFactory.CreateFunctionButton(".", "Decimal");
                button.Execute(this);
            }
            else if (e.Key == Key.Escape)
            {
                IButton button = _buttonFactory.CreateFunctionButton("C", "Clear");
                button.Execute(this);
            }
            else if (e.Key == Key.Back)
            {
                IButton button = _buttonFactory.CreateFunctionButton("⌫", "Delete");
                button.Execute(this);
            }

            // Закрываем Popup при нажатии клавиши
            if (ScientificPopup.IsOpen) ScientificPopup.IsOpen = false;
        }
    }
}