using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi
{
    public class ScadaDBContext : DbContext
    {
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<IOAddress> Addresses { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }
        public DbSet<RealTimeUnit> RealTimeUnits { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database\\database.sqlite");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AnalogInput>()
            //    .HasOne(ai => ai.Address)
            //    .WithMany()
            //    .HasForeignKey(ai => ai.AddressId);
            //modelBuilder.Entity<DigitalInput>()
            //    .HasOne(ai => ai.Address)
            //    .WithMany()
            //    .HasForeignKey(ai => ai.AddressId);
            //modelBuilder.Entity<AnalogOutput>()
            //    .HasOne(ai => ai.Address)
            //    .WithMany()
            //    .HasForeignKey(ai => ai.AddressId);
            //modelBuilder.Entity<DigitalOutput>()
            //    .HasOne(ai => ai.Address)
            //    .WithMany()
            //    .HasForeignKey(ai => ai.AddressId);
        }


    }
}