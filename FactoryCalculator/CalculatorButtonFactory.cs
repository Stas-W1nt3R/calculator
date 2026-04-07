using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FactoryCalculator
{
    public class CalculatorButtonFactory : IButtonFactory
    {
        public IButton CreateDigitButton(string digit)
        {
            return new DigitButton(digit);
        }

        public IButton CreateOperatorButton(string operation)
        {
            return new OperatorButton(operation);
        }

        public IButton CreateFunctionButton(string content, string functionType)
        {
            return new FunctionButton(content, functionType);
        }

        public IButton CreateScientificButton(string content, string functionType)
        {
            return new ScientificButton(content, functionType);
        }
    }
}