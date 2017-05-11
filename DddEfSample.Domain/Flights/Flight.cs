using System;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain.Flights
{
    public partial class Flight
    {
        public Flight(string departureCity, string arrivalCity, DateTimeOffset departingAt, Configuration configuration)
            : this(Guid.NewGuid(), DateTimeOffset.Now, DateTimeOffset.Now, departureCity, arrivalCity, departingAt, configuration, Enumerable.Empty<Booking>())
        {
        }

        public Flight(Guid id, DateTimeOffset createdAt, DateTimeOffset modifiedAt, string departureCity, string arrivalCity, DateTimeOffset departingAt, Configuration configuration, IEnumerable<Booking> bookings)
        {
            Id = id;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            DepartureCity = departureCity ?? throw new ArgumentNullException(nameof(departureCity));
            ArrivalCity = arrivalCity ?? throw new ArgumentNullException(nameof(arrivalCity));
            DepartingAt = departingAt;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _bookings = (bookings ?? throw new ArgumentNullException(nameof(bookings))).ToList();
        }

        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset ModifiedAt { get; }
        public string DepartureCity { get; }
        public string ArrivalCity { get; }
        public DateTimeOffset DepartingAt { get; }
    }
}
