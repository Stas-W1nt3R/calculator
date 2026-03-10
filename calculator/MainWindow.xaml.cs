using System;
using System.Windows;
using System.Windows.Controls;

namespace calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "";
        private string currentOperator = "";
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
                
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}