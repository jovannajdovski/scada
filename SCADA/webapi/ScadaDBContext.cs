using Microsoft.EntityFrameworkCore;
using webapi.model;
using webapi.Model;

namespace webapi
{
    public class ScadaDBContext : DbContext
    {
        public DbSet<TagBase> TagBases { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<IOAddress> Addresses { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }
        public DbSet<RealTimeUnit> RealTimeUnits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TagValue> TagValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database\\database.sqlite");
        }



    }
}