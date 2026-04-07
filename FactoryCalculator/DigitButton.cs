using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryCalculator
{
    public class DigitButton : ButtonBase
    {
        public DigitButton(string digit)
        {
            Content = digit;
            StyleKey = "NumberButtonStyle";
        }

        public override void Execute(MainWindow calculator)
        {
            if (calculator.IsNewCalculation ||
                calculator.CurrentInput == "0" ||
                calculator.IsOperatorJustPressed)
            {
                calculator.CurrentInput = Content;
                calculator.IsNewCalculation = false;
                calculator.IsOperatorJustPressed = false;
            }
            else
            {
                calculator.CurrentInput += Content;
            }

            calculator.DisplayTextBox.Text = calculator.CurrentInput;
        }
    }
}