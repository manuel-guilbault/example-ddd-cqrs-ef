using System;
using System.Threading.Tasks;

namespace DddEfSample.Domain.Flights
{
    public interface IFlightRepository
    {
        Task<Flight> GetByIdAsync(Guid id);
        Task CreateAsync(Flight flight);
        Task<Result<FlightUpdateError>> UpdateAsync(Flight flight);
    }

    public enum FlightUpdateError
    {
        NotFound,
        ConcurrencyConflict,
    }
}
