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
    public class FlightView : IFlightView
    {
        private readonly FlightDbContext _dbContext;

        public FlightView(FlightDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private async Task<IEnumerable<FlightProjection>> Query(Expression<Func<FlightRow, bool>> predicate = null)
        {
            Expression<Func<FlightRow, bool>> noPredicate = x => true;

            var rows = await _dbContext.Flights
                .Include(x => x.PhysicalClassCapacities)
                .AsNoTracking()
                .Where(predicate ?? noPredicate)
                .Select(row => new
                {
                    row.Id,
                    row.RowVersion,
                    row.Routing,
                    row.Schedule,
                    row.PhysicalClassCapacities,
                    PhysicalClassBookingSummaries = row.Bookings
                        .GroupBy(b => b.PhysicalClass)
                        .Select(g => new
                        {
                            PhysicalClass = g.Key,
                            NumberOfBookedSeats = g.Sum(b => b.NumberOfSeats)
                        })
                })
                .ToListAsync();

            return rows.Select(row => new FlightProjection(
                row.Id,
                row.RowVersion.ToETag(),
                row.Routing.ToDomain(),
                row.Schedule.ToDomain(),
                row.PhysicalClassCapacities.ToDomain(),
                new FlightBookingsSummary(row.PhysicalClassBookingSummaries
                    .Select(x => new PhysicalClassBookingsSummary(x.PhysicalClass, x.NumberOfBookedSeats)))
            ));
        }

        public async Task<IEnumerable<FlightProjection>> GetAllAsync()
        {
            var view = await Query();
            return view.ToList();
        }

        public async Task<FlightProjection> GetByIdAsync(Guid id)
        {
            var view = await Query(x => x.Id == id);
            return view.SingleOrDefault();
        }
    }
}
