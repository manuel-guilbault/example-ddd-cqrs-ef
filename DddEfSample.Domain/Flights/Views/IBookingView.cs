using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddEfSample.Domain.Flights.Views
{
    public interface IBookingView
    {
        Task<IEnumerable<BookingProjection>> GetAllForFlightAsync(Guid flightId);
        Task<BookingProjection> GetByIdAsync(Guid id);
    }
}
