using System;

namespace DddEfSample.Domain.Flights
{
    public class Booking
    {
        public Booking(PhysicalClassIataCode physicalClass, int numberOfSeats)
            : this(Guid.NewGuid(), DateTimeOffset.Now, physicalClass, numberOfSeats)
        {
        }

        public Booking(Guid id, DateTimeOffset bookedAt, PhysicalClassIataCode physicalClass, int numberOfSeats)
        {
            Id = id;
            BookedAt = bookedAt;
            PhysicalClass = physicalClass;
            NumberOfSeats = numberOfSeats;
        }

        public Guid Id { get; }
        public DateTimeOffset BookedAt { get; }
        public PhysicalClassIataCode PhysicalClass { get; }
        public int NumberOfSeats { get; }
    }
}
