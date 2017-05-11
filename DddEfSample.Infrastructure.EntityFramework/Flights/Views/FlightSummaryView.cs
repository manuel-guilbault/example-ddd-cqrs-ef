using DddEfSample.Domain.Flights;
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
    public class FlightSummaryView : IFlightSummaryView
    {
        private readonly FlightDbContext _dbContext;

        public FlightSummaryView(FlightDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private async Task<IEnumerable<FlightSummary>> Query(Expression<Func<FlightRow, bool>> predicate = null)
        {
            Expression<Func<FlightRow, bool>> noPredicate = x => true;

            var rows = await _dbContext.Flights
                .Include(x => x.PhysicalClassCapacities)
                .AsNoTracking()
                .Where(predicate ?? noPredicate)
                .Select(row => new
                {
                    row.Id,
                    row.CreatedAt,
                    row.ModifiedAt,
                    row.DepartureCity,
                    row.ArrivalCity,
                    row.DepartingAt,
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

            return rows.Select(row => new FlightSummary(
                row.Id,
                row.CreatedAt,
                row.ModifiedAt,
                row.DepartureCity,
                row.ArrivalCity,
                row.DepartingAt,
                new Configuration(
                    row.PhysicalClassCapacities.Select(x => new PhysicalClassCapacity(x.PhysicalClass, x.Capacity)).ToArray()
                ),
                new FlightBookingSummary(
                    row.PhysicalClassBookingSummaries.Select(x => new PhysicalClassBookingSummary(x.PhysicalClass, x.NumberOfBookedSeats)).ToArray()
                )
            ));
        }

        public async Task<IEnumerable<FlightSummary>> GetAll()
        {
            var view = await Query();
            return view.ToList();
        }

        public async Task<FlightSummary> GetById(Guid id)
        {
            var view = await Query(x => x.Id == id);
            return view.SingleOrDefault();
        }
    }
}
