using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights
{
    public static class OverBookingExtensions
    {
        public static bool IsOverBooked(this Configuration configuration, IEnumerable<Booking> bookings)
        {
            var bookedSeatsPerPhysicalClass = bookings
                .GroupBy(x => x.PhysicalClass)
                .ToDictionary(x => x.Key, x => x.Sum(b => b.NumberOfSeats));
            foreach (var physicalClassCapacity in configuration)
            {
                var bookedSeats = bookedSeatsPerPhysicalClass.GetOrDefault(physicalClassCapacity.PhysicalClass, 0);
                if (physicalClassCapacity.Capacity < bookedSeats)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
