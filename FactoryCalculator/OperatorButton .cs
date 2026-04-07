using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryCalculator
{
    public class OperatorButton : ButtonBase
    {
        public OperatorButton(string operation)
        {
            Content = operation;
            StyleKey = "OperatorButtonStyle";
        }

        public override void Execute(MainWindow calculator)
        {
            if (!calculator.IsOperatorJustPressed)
            {
                if (!string.IsNullOrEmpty(calculator.CurrentOperation))
                {
                    calculator.CalculateResult();
                }
                else
                {
                    calculator.FirstNumber = double.Parse(calculator.CurrentInput);
                }
            }

            calculator.CurrentOperation = Content;
            calculator.IsOperatorJustPressed = true;
        }
    }
}