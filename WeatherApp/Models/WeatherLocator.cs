using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherLocator
    {
        private readonly HttpClient _client;

        public WeatherLocator(HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
        }

        public async Task<LocationInfo> GetLocationAsync()
        {
            var response = await _client.GetStringAsync("http://ip-api.com/json/");
            var data = JsonDocument.Parse(response);

            return new LocationInfo(
                data.RootElement.GetProperty("lat").GetDouble(),
                data.RootElement.GetProperty("lon").GetDouble(),
                data.RootElement.GetProperty("city").GetString()!
            );
        }

        public async Task<WeatherInfo> GetWeatherAsync(double lat, double lon)
        {
            var url = FormattableString.Invariant(
                $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,wind_speed_10m&timezone=auto"
            );

            var response = await _client.GetStringAsync(url);
            var weather = JsonDocument.Parse(response);
            var current = weather.RootElement.GetProperty("current");

            return new WeatherInfo(
                current.GetProperty("temperature_2m").GetDouble(),
                current.GetProperty("wind_speed_10m").GetDouble()
            );
        }

        public async Task<(LocationInfo Location, WeatherInfo Weather)> GetLocationWithWeatherAsync()
        {
            var location = await GetLocationAsync();
            var weather = await GetWeatherAsync(location.Lat, location.Lon);
            return (location, weather);
        }
    }

    public record LocationInfo(double Lat, double Lon, string City);
    public record WeatherInfo(double Temperature, double WindSpeed);
}
