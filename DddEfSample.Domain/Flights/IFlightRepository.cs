using System;
using System.Threading.Tasks;

namespace DddEfSample.Domain.Flights
{
    public interface IFlightRepository
    {
        Task<Flight> GetByIdAsync(Guid id);
        Task CreateAsync(Flight flight);
        Task UpdateAsync(Flight flight);
    }
}
