using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Model
{
    //для запроса клиенту
    public class WeatherRequest
    {
        public string City { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
