using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddEfSample.Domain.Flights.Views
{
    public interface IFlightSummaryView
    {
        Task<IEnumerable<FlightSummary>> GetAll();
        Task<FlightSummary> GetById(Guid id);
    }
}
