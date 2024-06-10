using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Table
{
    public class Location
    {
        [Key]
        [Column("LocationID")]
        public int LocationID { get; set; }

        [Column("City", TypeName = "varchar(100)")]
        public string City { get; set; } = string.Empty;

        [Column("Country", TypeName = "varchar(100)")]
        public string Country { get; set; } = string.Empty;

        [Column("Latitude")]
        public decimal Latitude { get; set; }

        [Column("Longitude")]
        public decimal Longitude { get; set; }
    }
}
