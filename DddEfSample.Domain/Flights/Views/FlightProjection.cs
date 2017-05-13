using System;

namespace DddEfSample.Domain.Flights.Views
{
    public class FlightProjection
    {
        public FlightProjection(Guid id, string etag, Routing routing, Schedule schedule, Configuration configuration, FlightBookingsSummary bookingSummary)
        {
            Id = id;
            ETag = etag;
            Routing = routing ?? throw new ArgumentNullException(nameof(routing));
            Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            BookingSummary = bookingSummary ?? throw new ArgumentNullException(nameof(bookingSummary));
        }

        public Guid Id { get; }
        public string ETag { get; }
        public Routing Routing { get; }
        public Schedule Schedule { get; }
        public Configuration Configuration { get; }
        public FlightBookingsSummary BookingSummary { get; }
    }
}
