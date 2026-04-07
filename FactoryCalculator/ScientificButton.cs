using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace FactoryCalculator
{
    public class ScientificButton : ButtonBase
    {
        private string _functionType;

        public ScientificButton(string content, string functionType)
        {
            Content = content;
            _functionType = functionType;
            StyleKey = "FunctionButtonStyle";
        }

        public override void Execute(MainWindow calculator)
        {
            double currentNumber = double.Parse(calculator.CurrentInput);
            double result = 0;

            switch (_functionType)
            {
                case "Sqrt":
                    if (currentNumber < 0)
                    {
                        calculator.DisplayTextBox.Text = "Ошибка";
                        return;
                    }
                    result = Math.Sqrt(currentNumber);
                    break;

                case "Power":
                    result = Math.Pow(currentNumber, 2);
                    break;

                case "PowerY":
                    calculator.CurrentOperation = "^";
                    calculator.FirstNumber = currentNumber;
                    calculator.IsOperatorJustPressed = true;
                    calculator.DisplayTextBox.Text = currentNumber.ToString();
                    return;

                case "Log":
                    if (currentNumber <= 0)
                    {
                        calculator.DisplayTextBox.Text = "Ошибка";
                        return;
                    }
                    result = Math.Log10(currentNumber);
                    break;

                case "Ln":
                    if (currentNumber <= 0)
                    {
                        calculator.DisplayTextBox.Text = "Ошибка";
                        return;
                    }
                    result = Math.Log(currentNumber);
                    break;

                case "Sin":
                    result = Math.Sin(currentNumber * Math.PI / 180);
                    break;

                case "Cos":
                    result = Math.Cos(currentNumber * Math.PI / 180);
                    break;

                case "Tan":
                    result = Math.Tan(currentNumber * Math.PI / 180);
                    break;

                case "Percent":
                    result = currentNumber / 100;
                    break;

                case "MemorySave":
                    calculator.MemoryValue = currentNumber;
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    return;

                case "MemoryRecall":
                    calculator.CurrentInput = calculator.MemoryValue.ToString();
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    return;

                case "MemoryClear":
                    calculator.MemoryValue = 0;
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    return;

                case "MemoryAdd":
                    calculator.MemoryValue += currentNumber;
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    return;
            }

            calculator.CurrentInput = result.ToString();
            calculator.DisplayTextBox.Text = calculator.CurrentInput;
            calculator.IsNewCalculation = true;
        }
    }
}