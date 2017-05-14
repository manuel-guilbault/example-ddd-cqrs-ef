using DddEfSample.Domain.Flights.Views;
using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Views
{
    public class BookingView : IBookingView
    {
        private readonly FlightDbContext _dbContext;

        public BookingView(FlightDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private async Task<IEnumerable<BookingProjection>> Query(Expression<Func<BookingRow, bool>> predicate = null)
        {
            Expression<Func<BookingRow, bool>> noPredicate = x => true;

            var rows = await _dbContext.Bookings
                .AsNoTracking()
                .Where(predicate ?? noPredicate)
                .ToListAsync();

            return rows.Select(row => new BookingProjection(
                row.Id,
                row.FlightId,
                row.BookedAt,
                row.PhysicalClass,
                row.NumberOfSeats));
        }

        public async Task<IEnumerable<BookingProjection>> GetAllForFlightAsync(Guid flightId)
        {
            return await Query(x => x.FlightId == flightId);
        }

        public async Task<BookingProjection> GetByIdAsync(Guid id)
        {
            var result = await Query(x => x.Id == id);
            return result.SingleOrDefault();
        }
    }
}
