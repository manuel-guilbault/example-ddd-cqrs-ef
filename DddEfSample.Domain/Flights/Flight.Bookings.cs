using System;
using System.Collections.Generic;

namespace DddEfSample.Domain.Flights
{
    public partial class Flight
    {

        private List<Booking> _bookings;
        public IReadOnlyList<Booking> Bookings => _bookings;

        public Result<BookError> Book(PhysicalClassIataCode physicalClass, int numberOfSeats)
        {
            var newBooking = new Booking(physicalClass, numberOfSeats);

            var bookingsSimulation = _bookings.Concat(newBooking);
            if (Configuration.IsOverBooked(bookingsSimulation))
            {
                return Result.Failure(BookError.NoMoreCapacity);
            }

            _bookings.Add(newBooking);
            return Result.Success<BookError>();
        }

        public enum BookError
        {
            NoMoreCapacity,
        }
    }
}
