using System.Windows.Input;

namespace SimpleCalculatorMVVM.Models
{
    public class ButtonInfo
    {
        public string Content { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; } = 1;
        public int ColumnSpan { get; set; } = 1;
        public ICommand Command { get; set; }
        public string CommandParameter { get; set; }
        public string StyleKey { get; set; } = "NumberButtonStyle";
    }
}