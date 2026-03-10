using System;
using System.Windows;
using System.Windows.Controls;

namespace calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string currentOperation = "";
        private double firstNumber = 0;
        private bool isNewCalculation = true;
        private bool isOperatorJustPressed = false;

        public MainWindow()
        {
            InitializeComponent();
            DisplayTextBox.Text = currentInput;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content?.ToString() ?? "";

            if(isNewCalculation || currentInput == "0" || isOperatorJustPressed)
            {
                currentInput = digit;
                isNewCalculation = false;
                isOperatorJustPressed= false;
            }
            else
            {
                currentInput += digit;
            }

            DisplayTextBox.Text = currentInput;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content?.ToString() ?? "";

            if (!isOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(currentOperation))
                {
                    CalculateResult();
                }
                else
                {
                    firstNumber = double.Parse(currentInput);   
                }
            }
            currentOperation = operation;
            isOperatorJustPressed = true;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentOperation) && !isOperatorJustPressed)
            {
                CalculateResult();
                currentOperation = "";
                isNewCalculation = true;
            }
        }

        private void CalculateResult()
        {
            double secondNumber = double.Parse(currentInput);
            double result = 0;

            switch (currentOperation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "x":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        result = firstNumber / secondNumber;
                    }
                        break;
            }
            currentInput = result.ToString();

            DisplayTextBox.Text = currentInput;
            firstNumber = result;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            currentOperation = "";
            firstNumber = 0;
            isNewCalculation = true;
            isOperatorJustPressed = false;
            DisplayTextBox.Text = currentInput;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOperatorJustPressed)
            {
                currentInput = "0,";
                isOperatorJustPressed = false;
            }
            else if (!currentInput.Contains(',')){
                isNewCalculation = false;
                currentInput += ",";
            }
            
            DisplayTextBox.Text = currentInput;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperatorJustPressed && currentInput.Length > 0 && !isNewCalculation)
            {
                currentInput = currentInput[..^1];
                if (string.IsNullOrEmpty(currentInput) || currentInput == "-")
                    currentInput = "0";
            }   DisplayTextBox.Text = currentInput;
        }

        private void PlusMinusButton_Click(object sender,RoutedEventArgs e)
        {
            if (!isOperatorJustPressed && !isNewCalculation && currentInput != "0")
            {
                currentInput = currentInput.StartsWith('-') ? currentInput[1..] : "-" + currentInput;
                DisplayTextBox.Text = currentInput;
            }
        }
    }
}