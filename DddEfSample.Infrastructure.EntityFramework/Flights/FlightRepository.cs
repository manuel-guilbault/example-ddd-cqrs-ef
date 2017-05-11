using DddEfSample.Domain.Flights;
using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DddEfSample.Infrastructure.EntityFramework.Flights
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightDbContext _dbContext;

        public FlightRepository(FlightDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private async Task<FlightRow> FindRowByIdAsync(Guid id)
        {
            return await _dbContext.Flights
                .Include(x => x.PhysicalClassCapacities)
                .Include(x => x.Bookings)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Flight> GetByIdAsync(Guid id)
        {
            var row = await FindRowByIdAsync(id);
            if (row == null)
            {
                return null;
            }

            var flight = MapFromDb(row);
            return flight;
        }

        public async Task CreateAsync(Flight flight)
        {
            if (flight == null) throw new ArgumentNullException(nameof(flight));

            var row = new FlightRow();
            MapToDb(row, flight);

            _dbContext.Flights.Add(row);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            if (flight == null) throw new ArgumentNullException(nameof(flight));

            var row = await FindRowByIdAsync(flight.Id);
            if (row == null)
            {
                throw new ArgumentException($"Flight {flight.Id} not found");
            }

            MapToDb(row, flight);
            row.ModifiedAt = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();
        }

        private Flight MapFromDb(FlightRow row)
        {
            return new Flight(
                row.Id,
                row.CreatedAt,
                row.ModifiedAt,
                row.DepartureCity,
                row.ArrivalCity,
                row.DepartingAt,
                new Configuration(
                    row.PhysicalClassCapacities
                        .Select(x => new PhysicalClassCapacity(x.PhysicalClass, x.Capacity))
                        .ToArray()
                ),
                row.Bookings
                    .Select(x => new Booking(x.Id, x.BookedAt, x.PhysicalClass, x.NumberOfSeats))
            );
        }

        private void MapToDb(FlightRow row, Flight flight)
        {
            row.Id = flight.Id;
            row.CreatedAt = flight.CreatedAt;
            row.ModifiedAt = flight.ModifiedAt;
            row.DepartureCity = flight.DepartureCity;
            row.ArrivalCity = flight.ArrivalCity;
            row.DepartingAt = flight.DepartingAt;
            row.PhysicalClassCapacities.SynchronizeWith(flight.Configuration, x => x.PhysicalClass, x => x.PhysicalClass, (x, y) => MapToDb(x, y, flight));
            row.Bookings.SynchronizeWith(flight.Bookings, x => x.Id, x => x.Id, (x, y) => MapToDb(x, y, flight));
        }

        private void MapToDb(PhysicalClassCapacityRow row, PhysicalClassCapacity physicalClassCapacity, Flight flight)
        {
            row.FlightId = flight.Id;
            row.PhysicalClass = physicalClassCapacity.PhysicalClass;
            row.Capacity = physicalClassCapacity.Capacity;
        }

        private void MapToDb(BookingRow row, Booking booking, Flight flight)
        {
            row.Id = booking.Id;
            row.FlightId = flight.Id;
            row.BookedAt = booking.BookedAt;
            row.PhysicalClass = booking.PhysicalClass;
            row.NumberOfSeats = booking.NumberOfSeats;
        }
    }
}
