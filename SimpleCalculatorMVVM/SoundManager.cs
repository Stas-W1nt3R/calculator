// SoundManager.cs
using System;
using System.Media;
using System.Windows;

namespace SimpleCalculatorMVVM
{
    public static class SoundManager
    {
        private static bool _enabled = true;

        public static bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        public static void PlayClick()
        {
            if (!_enabled) return;

            try
            {
                var stream = Application.GetResourceStream(
                    new Uri("pack://application:,,,/click.wav"));

                if (stream != null)
                {
                    var player = new SoundPlayer(stream.Stream);
                    player.Play(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка звука: {ex.Message}");
            }
        }
    }
}