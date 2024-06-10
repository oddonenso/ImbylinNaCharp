using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.Model;

namespace WeatherLibrary.Interfaces
{
    public interface IWeatherService
    {
        WeatherResponse GetWeather(WeatherRequest request);
    }
}
