using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights.Views
{
    public class FlightBookingSummary : IEnumerable<PhysicalClassBookingSummary>
    {
        private readonly Dictionary<PhysicalClassIataCode, PhysicalClassBookingSummary> _bookingSummaries;

        public FlightBookingSummary(params PhysicalClassBookingSummary[] bookingSummaries)
        {
            _bookingSummaries = bookingSummaries.ToDictionary(x => x.PhysicalClass);
        }

        public int TotalNumberOfBookedSeats => _bookingSummaries.Values.Sum(x => x.NumberOfBookedSeats);
        
        public IEnumerator<PhysicalClassBookingSummary> GetEnumerator()
        {
            return _bookingSummaries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
