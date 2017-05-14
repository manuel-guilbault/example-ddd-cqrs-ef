using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System.Data.Entity;

namespace DddEfSample.Infrastructure.EntityFramework.Flights
{
    public class FlightDbContext: DbContext
    {
        public DbSet<FlightRow> Flights { get; set; }
        public DbSet<PhysicalClassCapacityRow> PhysicalClassCapacities { get; set; }
        public DbSet<BookingRow> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var flightModel = modelBuilder.Entity<FlightRow>().ToTable("Flights");
            flightModel.HasKey(x => x.Id);
            flightModel.Property(x => x.RowVersion).IsRowVersion();
            flightModel.HasMany(x => x.PhysicalClassCapacities).WithRequired().HasForeignKey(x => x.FlightId).WillCascadeOnDelete();
            flightModel.HasMany(x => x.Bookings).WithRequired().HasForeignKey(x => x.FlightId).WillCascadeOnDelete();

            var routingModel = modelBuilder.ComplexType<RoutingValues>();
            routingModel.Property(x => x.DepartureCity).HasMaxLength(100).IsRequired();
            routingModel.Property(x => x.ArrivalCity).HasMaxLength(100).IsRequired();

            var scheduleModel = modelBuilder.ComplexType<ScheduleValues>();

            var physicalClassCapacityModel = modelBuilder.Entity<PhysicalClassCapacityRow>().ToTable("PhysicalClassCapacities");
            physicalClassCapacityModel.HasKey(x => new { x.FlightId, x.PhysicalClass });

            var bookingModel = modelBuilder.Entity<BookingRow>().ToTable("Bookings");
            bookingModel.HasKey(x => x.Id);
        }
    }
}
