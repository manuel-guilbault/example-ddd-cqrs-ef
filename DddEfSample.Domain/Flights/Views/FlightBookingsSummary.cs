using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights.Views
{
    public class FlightBookingsSummary : IEnumerable<PhysicalClassBookingsSummary>
    {
        private readonly Dictionary<PhysicalClassIataCode, PhysicalClassBookingsSummary> _bookingSummaries;

        public FlightBookingsSummary(IEnumerable<PhysicalClassBookingsSummary> bookingSummaries)
        {
            if (bookingSummaries == null) { throw new ArgumentNullException(nameof(bookingSummaries)); }

            _bookingSummaries = bookingSummaries.ToDictionary(x => x.PhysicalClass);
        }

        public int TotalNumberOfBookedSeats => _bookingSummaries.Values.Sum(x => x.NumberOfBookedSeats);
        
        public IEnumerator<PhysicalClassBookingsSummary> GetEnumerator()
        {
            return _bookingSummaries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
