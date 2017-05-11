using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DddEfSample.Infrastructure.EntityFramework.Flights
{
    public class FlightDbContext: DbContext
    {
        public DbSet<FlightRow> Flights { get; set; }
        public DbSet<PhysicalClassCapacityRow> PhysicalClassCapacities { get; set; }
        public DbSet<BookingRow> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var flightModel = modelBuilder.Entity<FlightRow>();
            flightModel.HasKey(x => x.Id);
            flightModel.Property(x => x.DepartureCity).HasMaxLength(100).IsRequired();
            flightModel.Property(x => x.ArrivalCity).HasMaxLength(100).IsRequired();
            flightModel.HasMany(x => x.PhysicalClassCapacities).WithRequired().HasForeignKey(x => x.FlightId).WillCascadeOnDelete();
            flightModel.HasMany(x => x.Bookings).WithRequired().HasForeignKey(x => x.FlightId).WillCascadeOnDelete();

            modelBuilder.Entity<PhysicalClassCapacityRow>()
                .HasKey(x => new { x.FlightId, x.PhysicalClass });

            modelBuilder.Entity<BookingRow>()
                .HasKey(x => x.Id);
        }
    }
}
