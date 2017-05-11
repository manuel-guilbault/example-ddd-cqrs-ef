using System;
using System.Collections.Generic;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Entities
{
    public class FlightRow
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTimeOffset DepartingAt { get; set; }
        public virtual ICollection<PhysicalClassCapacityRow> PhysicalClassCapacities { get; set; } = new List<PhysicalClassCapacityRow>();
        public virtual ICollection<BookingRow> Bookings { get; set; } = new List<BookingRow>();
    }
}
