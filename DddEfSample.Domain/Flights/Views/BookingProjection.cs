using System;

namespace DddEfSample.Domain.Flights.Views
{
    public class BookingProjection
    {
        public BookingProjection(Guid id, Guid flightId, DateTimeOffset bookedAt, PhysicalClassIataCode physicalClass, int numberOfSeats)
        {
            Id = id;
            FlightId = flightId;
            BookedAt = bookedAt;
            PhysicalClass = physicalClass;
            NumberOfSeats = numberOfSeats;
        }

        public Guid Id { get; }
        public Guid FlightId { get; }
        public DateTimeOffset BookedAt { get; }
        public PhysicalClassIataCode PhysicalClass { get; }
        public int NumberOfSeats { get; }
    }
}
