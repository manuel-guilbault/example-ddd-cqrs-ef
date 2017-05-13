using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddEfSample.Domain.Flights.Views
{
    public interface IFlightView
    {
        Task<IEnumerable<FlightProjection>> GetAllAsync();
        Task<FlightProjection> GetByIdAsync(Guid id);
    }
}
