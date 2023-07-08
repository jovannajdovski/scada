using Microsoft.EntityFrameworkCore;
using SimulationDriver.model;
using System.Security.Claims;

namespace SimulationDriver
{
    public class SimulatorDBContext : DbContext
    {
        public DbSet<IOAdress> Adresses { get; set; }
        public DbSet<RealTimeUnit> RealTimeUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database\\database.sqlite");
            
        }
    }
}