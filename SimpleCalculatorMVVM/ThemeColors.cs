using System.Windows.Media;

namespace SimpleCalculatorMVVM
{
    public static class ThemeColors
    {
        public static class Light
        {
            public static string Background = "#F0F4F8";
            public static string ButtonNumber = "#E3F2FD";
            public static string ButtonOperator = "#BBDEFB";
            public static string ButtonEquals = "#2196F3";
            public static string Text = "#212121";
            public static string DisplayBackground = "#FFFFFF";
        }

        public static class Dark
        {
            public static string Background = "#2D2D2D";
            public static string ButtonNumber = "#424242";
            public static string ButtonOperator = "#616161";
            public static string ButtonEquals = "#FF9800";
            public static string Text = "#FFFFFF";
            public static string DisplayBackground = "#1E1E1E";
        }

        public static class HighContrast
        {
            public static string Background = "#000000";
            public static string ButtonNumber = "#000000";
            public static string ButtonOperator = "#000000";
            public static string ButtonEquals = "#000000";
            public static string Text = "#FFFF00";
            public static string DisplayBackground = "#000000";
        }
    }
}