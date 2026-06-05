using Avalonia.Controls;
using Microsoft.VisualBasic;
using ReactiveUI;
using Splat;
using System;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly WeatherLocator _weatherLocator = new WeatherLocator();

        public MainWindowViewModel()
        {
            LoadWeatherAsync();
        }

        private string _greeting = "Загрузка...";
        public string Greeting
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }

        private async void LoadWeatherAsync()
        {
            try
            {
                var (location, weather) = await _weatherLocator.GetLocationWithWeatherAsync();
                Greeting = $"{location.City}: {weather.Temperature}°C, ветер {weather.WindSpeed} км/ч";
            }
            catch (Exception ex)
            {
                Greeting = $"Ошибка: {ex.Message}";
            }
        }
    }
}
