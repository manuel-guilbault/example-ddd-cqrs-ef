using System;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights
{
    public partial class Flight
    {
        public Flight(Routing routing, Schedule schedule, Configuration configuration)
            : this(Guid.NewGuid(), null, routing, schedule, configuration, Enumerable.Empty<Booking>())
        {
        }

        public Flight(Guid id, string etag, Routing routing, Schedule schedule, Configuration configuration, IEnumerable<Booking> bookings)
        {
            Id = id;
            ETag = etag;
            Routing = routing ?? throw new ArgumentNullException(nameof(routing));
            Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _bookings = (bookings ?? throw new ArgumentNullException(nameof(bookings))).ToList();
        }

        public Guid Id { get; }
        public string ETag { get; }
    
        public Routing Routing { get; }
        public Schedule Schedule { get; }
    }
}
