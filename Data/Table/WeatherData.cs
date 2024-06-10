using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Table
{
    public class WeatherData
    {
        [Key]
        [Column("WeatherDataID")]
        public int WeatherDataID { get; set; }

        [Column("LocationID")]
        [ForeignKey("Location")]
        public int LocationID { get; set; }

        public Location? Location { get; set; }

        [Column("WeatherTypeID")]
        [ForeignKey("WeatherType")]
        public int WeatherTypeID { get; set; }

        public WeatherType? WeatherType { get; set; }

        [Column("Temperature")]
        public decimal Temperature { get; set; }

        //влажность
        [Column("Humidity")]
        public decimal Humidity { get; set; }

        //давление
        [Column("Pressure")]
        public decimal Pressure { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }
    }
}
