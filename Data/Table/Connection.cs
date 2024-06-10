using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Table
{
    public class Connection: DbContext
    {
        public Connection(DbContextOptions<Connection> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<WeatherType> WeatherTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=111;Database=PrognozPogodi;");
        }
    }
}
