using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.IO;
using System.Windows;
using System.Xml;

namespace SimpleCalculatorMVVM
{
    public class AppConfig
    {
        public int WindowWidth { get; set; } = 400;
        public int WindowHeight { get; set; } = 650;
        public string BackgroundColor { get; set; } = "#F0F4F8";
        public int FontSize { get; set; } = 16;
        public string ButtonColor { get; set; } = "#2196F3";
        public bool SoundEnabled { get; set; } = true;
        public string CursorType { get; set; } = "Hand";
        public string Theme { get; set; } = "Light";

        private static AppConfig _current;

        public static AppConfig Current
        {
            get
            {
                if (_current == null)
                    Load();
                return _current;
            }
        }

        public static void Load()
        {
            string path = "config.json";

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    _current = JsonConvert.DeserializeObject < AppConfig > (json);
                }
                catch
                {
                    MessageBox.Show("Ошибка загрузки config.json. Используются настройки по умолчанию.",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _current = new AppConfig();
                }
            }
            else
            {
                _current = new AppConfig();
                Save();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(_current, Formatting.Indented);
            File.WriteAllText("config.json", json);
        }
    }
}