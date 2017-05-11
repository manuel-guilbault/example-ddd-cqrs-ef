using DddEfSample.Domain.Flights;
using System;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Entities
{
    public class BookingRow
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public DateTimeOffset BookedAt { get; set; }
        public PhysicalClassIataCode PhysicalClass { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
