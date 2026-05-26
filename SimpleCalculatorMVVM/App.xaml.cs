using System.Windows;

namespace SimpleCalculatorMVVM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Загружаем конфигурацию при старте
            AppConfig.Load();

            // Применяем настройки звука
            SoundManager.Enabled = AppConfig.Current.SoundEnabled;
        }
    }
}