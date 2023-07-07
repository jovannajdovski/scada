using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi
{
    public class ScadaDBContext : DbContext
    {
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<IOAdress> Adresses { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database\\database.sqlite");
        }
    }
}