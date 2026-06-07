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
        public WeatherWidgetViewModel Weather { get; } = new WeatherWidgetViewModel();

        public MainWindowViewModel()
        {
          
        }

        private string _greeting = "Загрузка...";
        public string Greeting
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }

       
    }
}
