using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights
{
    public static class OverBookingExtensions
    {
        public static bool IsOverBooked(this Configuration configuration, IEnumerable<Booking> bookings)
        {
            var bookSeatsByPhysicalClass = bookings
                .GroupBy(x => x.PhysicalClass)
                .ToDictionary(x => x.Key, x => x.Sum(b => b.NumberOfSeats));
            foreach (var physicalClassCapacity in configuration)
            {
                if (physicalClassCapacity.Capacity < bookSeatsByPhysicalClass.GetOrDefault(physicalClassCapacity.PhysicalClass, 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
