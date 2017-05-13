using System;
using System.Collections.Generic;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Entities
{
    public class FlightRow
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
        public RoutingValues Routing { get; set; }
        public ScheduleValues Schedule { get; set; }
        public virtual ICollection<PhysicalClassCapacityRow> PhysicalClassCapacities { get; set; } = new List<PhysicalClassCapacityRow>();
        public virtual ICollection<BookingRow> Bookings { get; set; } = new List<BookingRow>();
    }
}
