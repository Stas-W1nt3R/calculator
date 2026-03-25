using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FactoryCalculator
{
    public interface IButton
    {
        string GetContent();
        string GetStyle();
        void Execute(MainWindow calculator);
    }
}