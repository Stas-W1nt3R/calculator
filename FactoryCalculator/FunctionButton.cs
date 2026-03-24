using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryCalculator
{
    public class FunctionButton : ButtonBase
    {
        private string _functionType;

        public FunctionButton(string content, string functionType)
        {
            Content = content;
            _functionType = functionType;

            if (content == "=")
                StyleKey = "EqualsButtonStyle";
            else
                StyleKey = "FunctionButtonStyle";
        }

        public override void Execute(MainWindow calculator)
        {
            switch (_functionType)
            {
                case "Clear":
                    calculator.CurrentInput = "0";
                    calculator.CurrentOperation = "";
                    calculator.FirstNumber = 0;
                    calculator.IsNewCalculation = true;
                    calculator.IsOperatorJustPressed = false;
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    break;

                case "Delete":
                    if (!calculator.IsOperatorJustPressed &&
                        calculator.CurrentInput.Length > 0 &&
                        !calculator.IsNewCalculation)
                    {
                        calculator.CurrentInput = calculator.CurrentInput[..^1];
                        if (string.IsNullOrEmpty(calculator.CurrentInput) ||
                            calculator.CurrentInput == "-")
                            calculator.CurrentInput = "0";
                    }
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    break;

                case "PlusMinus":
                    if (!calculator.IsOperatorJustPressed &&
                        !calculator.IsNewCalculation &&
                        calculator.CurrentInput != "0")
                    {
                        calculator.CurrentInput = calculator.CurrentInput.StartsWith('-')
                            ? calculator.CurrentInput[1..]
                            : "-" + calculator.CurrentInput;
                        calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    }
                    break;

                case "Equals":
                    if (!string.IsNullOrEmpty(calculator.CurrentOperation) &&
                        !calculator.IsOperatorJustPressed)
                    {
                        calculator.CalculateResult();
                        calculator.CurrentOperation = "";
                        calculator.IsNewCalculation = true;
                    }
                    break;

                case "Decimal":
                    if (calculator.IsOperatorJustPressed)
                    {
                        calculator.CurrentInput = "0,";
                        calculator.IsOperatorJustPressed = false;
                    }
                    else if (!calculator.CurrentInput.Contains(','))
                    {
                        calculator.IsNewCalculation = false;
                        calculator.CurrentInput += ",";
                    }
                    calculator.DisplayTextBox.Text = calculator.CurrentInput;
                    break;
            }
        }
    }
}