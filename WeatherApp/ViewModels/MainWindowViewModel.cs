using Avalonia.Controls;
using Microsoft.VisualBasic;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public WeatherWidgetViewModel Weather { get; } = new WeatherWidgetViewModel();
        private string _time = DateTime.Now.ToString("HH:mm");
        public string Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }
        public MainWindowViewModel()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                  .ObserveOn(RxApp.MainThreadScheduler)
                  .Subscribe(_ => Time = DateTime.Now.ToString("HH:mm"));
        }

        

       
    }
}
