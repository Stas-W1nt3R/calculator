using System;
using System.Windows;

namespace calculator
{
    public partial class MainWindow : Window
    {
        private string currentNumber = "";
        private string operation = "";
        private double firstNumber = 0;
        private bool isNewNumber = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Нажатие на цифру
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            string digit = button.Content.ToString();

            if (isNewNumber)
            {
                Display.Text = digit;
                isNewNumber = false;
            }
            else
            {
                Display.Text += digit;
            }
        }

        // Нажатие на операцию (+, -, ×, ÷)
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            firstNumber = double.Parse(Display.Text);
            operation = button.Content.ToString();
            isNewNumber = true;
        }

        // Равно
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double secondNumber = double.Parse(Display.Text);
            double result = 0;

            switch (operation)
            {
                case "+": result = firstNumber + secondNumber; break;
                case "-": result = firstNumber - secondNumber; break;
                case "×": result = firstNumber * secondNumber; break;
                case "÷":
                    if (secondNumber != 0)
                        result = firstNumber / secondNumber;
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя!");
                        return;
                    }
                    break;
                case "%": result = firstNumber % secondNumber; break;
            }

            Display.Text = result.ToString();
            isNewNumber = true;
        }

        // Очистка
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            currentNumber = "";
            operation = "";
            firstNumber = 0;
            isNewNumber = true;
        }

        // Стереть последний символ
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 1)
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            else
                Display.Text = "0";
        }
    }
}