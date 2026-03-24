using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryCalculator
{
    public abstract class ButtonBase : IButton
    {
        protected string Content { get; set; }
        protected string StyleKey { get; set; }

        public virtual string GetContent()
        {
            return Content;
        }

        public virtual string GetStyle()
        {
            return StyleKey;
        }

        public abstract void Execute(MainWindow calculator);
    }
}
