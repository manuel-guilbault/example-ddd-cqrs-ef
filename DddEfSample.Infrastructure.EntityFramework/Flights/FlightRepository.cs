using DddEfSample.Domain;
using DddEfSample.Domain.Flights;
using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

            var flight = row.ToDomain();
            return flight;
        }

        public async Task CreateAsync(Flight flight)
        {
            if (flight == null) throw new ArgumentNullException(nameof(flight));

            var row = new FlightRow();
            flight.MapTo(row);

            _dbContext.Flights.Add(row);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Result<FlightUpdateError>> UpdateAsync(Flight flight)
        {
            if (flight == null) throw new ArgumentNullException(nameof(flight));

            var row = await FindRowByIdAsync(flight.Id);
            if (row == null)
            {
                return Result.Failure(FlightUpdateError.NotFound);
            }

            flight.MapTo(row);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure(FlightUpdateError.ConcurrencyConflict);
            }

            return Result.Success<FlightUpdateError>();
        }
    }
}
