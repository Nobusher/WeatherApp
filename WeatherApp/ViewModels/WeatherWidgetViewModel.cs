using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    internal class WeatherWidgetViewModel : ViewModelBase
    {
        private readonly WeatherLocator _weatherLocator = new WeatherLocator();
        public WeatherWidgetViewModel()
        {
            LoadWeatherAsync();
        }
        private string _city = "-";
        public string City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }
        private string _temperature = "-";

        public string Temperature
        {
            get => _temperature;
            set => this.RaiseAndSetIfChanged(ref _temperature, value);
        }
        private string _wind = "-";

        public string Wind
        {
            get => _wind;
            set => this.RaiseAndSetIfChanged(ref _wind, value);
        }
        private string _status = "Loading...";

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        private bool _isLoading = true;

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }
        private async void LoadWeatherAsync()
        {
            try
            {
                var (location, weather) = await _weatherLocator.GetLocationWithWeatherAsync();
                City = location.City;
                Temperature = $"🌡️{weather.Temperature}°C";
                Wind = $"{weather.WindSpeed}km/h";
                IsLoading = false;
               
            }
            catch (Exception ex)
            {
                Status = $"Error: {ex.Message}";
                IsLoading= false;
            }
        }
    }
}
