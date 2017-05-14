using System;
using System.Collections.Generic;

namespace DddEfSample.Domain.Flights
{
    public partial class Flight
    {

        private List<Booking> _bookings;
        public IReadOnlyList<Booking> Bookings => _bookings;

        public Result<BookingError> Book(Booking booking)
        {
            var bookingsSimulation = _bookings.Concat(booking);
            if (Configuration.IsOverBooked(bookingsSimulation))
            {
                return Result.Failure(BookingError.NoMoreCapacity);
            }

            _bookings.Add(booking);
            return Result.Success<BookingError>();
        }

        public enum BookingError
        {
            NoMoreCapacity,
        }
    }
}
