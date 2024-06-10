using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Table
{
    public class WeatherType
    {
        [Key]
        [Column("WeatherTypeID")]
        public int WeatherTypeID { get; set; }

        [Column("Description", TypeName = "varchar(100)")]
        public string Description { get; set; } = string.Empty;
    }
}
