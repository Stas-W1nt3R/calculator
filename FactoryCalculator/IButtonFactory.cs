using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryCalculator
{
    public interface IButtonFactory
    {
        IButton CreateDigitButton(string digit);
        IButton CreateOperatorButton(string operation);
        IButton CreateFunctionButton(string content, string functionType);
        IButton CreateScientificButton(string content, string functionType);
    }
}