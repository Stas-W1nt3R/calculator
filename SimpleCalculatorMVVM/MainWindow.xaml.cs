using SimpleCalculatorMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static SimpleCalculatorMVVM.ThemeColors;

namespace SimpleCalculatorMVVM
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;

            // ← ПОДПИСКА на смену темы
            _viewModel.ThemeChanged += (s, theme) => ApplyTheme(theme);

            ApplyConfig();
        }

        private void ApplyConfig()
        {
            var config = AppConfig.Current;

            this.Width = config.WindowWidth;
            this.Height = config.WindowHeight;

            try
            {
                this.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(config.BackgroundColor));
            }
            catch { }

            this.FontSize = config.FontSize;
            this.Cursor = GetCursorFromConfig();

            ApplyTheme(config.Theme);
        }

        private void ApplyTheme(string theme)
        {
            string bg, btnNum, btnOp, btnEq, text, displayBg;

            switch (theme)
            {
                case "Dark":
                    bg = ThemeColors.Dark.Background;
                    btnNum = ThemeColors.Dark.ButtonNumber;
                    btnOp = ThemeColors.Dark.ButtonOperator;
                    btnEq = ThemeColors.Dark.ButtonEquals;
                    text = ThemeColors.Dark.Text;
                    displayBg = ThemeColors.Dark.DisplayBackground;
                    break;

                case "HighContrast":
                    bg = ThemeColors.HighContrast.Background;
                    btnNum = ThemeColors.HighContrast.ButtonNumber;
                    btnOp = ThemeColors.HighContrast.ButtonOperator;
                    btnEq = ThemeColors.HighContrast.ButtonEquals;
                    text = HighContrast.Text;
                    displayBg = ThemeColors.HighContrast.DisplayBackground;
                    break;

                default:
                    bg = ThemeColors.Light.Background;
                    btnNum = ThemeColors.Light.ButtonNumber;
                    btnOp = ThemeColors.Light.ButtonOperator;
                    btnEq = ThemeColors.Light.ButtonEquals;
                    text = ThemeColors.Light.Text;
                    displayBg = ThemeColors.Light.DisplayBackground;
                    break;
            }

            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bg));

            foreach (var button in FindVisualChildren<Button>(this))
            {
                string content = button.Content?.ToString() ?? "";

                if (content == "=")
                {
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(btnEq));
                }
                else if ("+-×÷".Contains(content))
                {
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(btnOp));
                }
                else
                {
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(btnNum));
                }

                button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(text));
            }

            foreach (var textBox in FindVisualChildren<TextBox>(this))
            {
                textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(displayBg));
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(text));
            }
        }

        private Cursor GetCursorFromConfig()
        {
            switch (AppConfig.Current.CursorType)
            {
                case "Hand": return Cursors.Hand;
                case "Cross": return Cursors.Cross;
                case "IBeam": return Cursors.IBeam;
                default: return Cursors.Arrow;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_viewModel.IsScientificPopupOpen)
            {
                if (e.Key == Key.Escape)
                {
                    _viewModel.ToggleScientificCommand.Execute(null);
                    e.Handled = true;
                }
                return;
            }

            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_viewModel.CanUndo)
                    _viewModel.UndoCommand.Execute(null);
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Y && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_viewModel.CanRedo)
                    _viewModel.RedoCommand.Execute(null);
                e.Handled = true;
                return;
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = e.Key.ToString().Last().ToString();
                SoundManager.PlayClick();
                _viewModel.DigitCommand.Execute(digit);
                e.Handled = true;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                SoundManager.PlayClick();
                _viewModel.DigitCommand.Execute(digit);
                e.Handled = true;
            }
            else if (e.Key == Key.Add)
            {
                SoundManager.PlayClick();
                _viewModel.OperatorCommand.Execute("+");
                e.Handled = true;
            }
            else if (e.Key == Key.Subtract)
            {
                SoundManager.PlayClick();
                _viewModel.OperatorCommand.Execute("-");
                e.Handled = true;
            }
            else if (e.Key == Key.Multiply)
            {
                SoundManager.PlayClick();
                _viewModel.OperatorCommand.Execute("x");
                e.Handled = true;
            }
            else if (e.Key == Key.Divide)
            {
                SoundManager.PlayClick();
                _viewModel.OperatorCommand.Execute("÷");
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                SoundManager.PlayClick();
                _viewModel.FunctionCommand.Execute("Equals");
                e.Handled = true;
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                SoundManager.PlayClick();
                _viewModel.FunctionCommand.Execute("Decimal");
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                SoundManager.PlayClick();
                _viewModel.FunctionCommand.Execute("Clear");
                e.Handled = true;
            }
            else if (e.Key == Key.Back)
            {
                SoundManager.PlayClick();
                _viewModel.FunctionCommand.Execute("Delete");
                e.Handled = true;
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;

                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }
    }
}