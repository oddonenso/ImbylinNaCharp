using Data.Table;
using Microsoft.EntityFrameworkCore;
using WeatherLibrary.Interfaces;
using WeatherLibrary.Model;

namespace WeatherLibrary.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly Connection _context;

        public WeatherService(Connection context)
        {
            _context = context;
        }
        //запросик на погоду
        public WeatherResponse GetWeather(WeatherRequest request)
        {
            var weatherData = _context.WeatherData
                .Include(w => w.Location)
                .Include(w => w.WeatherType)
                .FirstOrDefault(w => w.Location.City == request.City && w.Date.Date == request.Date.Date);

            if (weatherData == null)
            {
                //если данные не найдены
                return new WeatherResponse
                {
                    City = request.City,
                    Date = request.Date,
                    Temperature = 0,
                    Humidity = 0,
                    Pressure = 0,
                    WeatherType = "Данные не найдены"
                };
            }

            return new WeatherResponse
            {
                City = weatherData.Location.City,
                Date = weatherData.Date,
                Temperature = weatherData.Temperature,
                Humidity = weatherData.Humidity,
                Pressure = weatherData.Pressure,
                WeatherType = weatherData.WeatherType.Description
            };
        }
    }
}
