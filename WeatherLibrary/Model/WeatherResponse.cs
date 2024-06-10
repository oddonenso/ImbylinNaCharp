using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Model
{
    //ответка
    public class WeatherResponse
    {
        public string City { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Pressure { get; set; }
        public string WeatherType { get; set; } = string.Empty;
    }
}
