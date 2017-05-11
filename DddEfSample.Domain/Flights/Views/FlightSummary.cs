using System;

namespace DddEfSample.Domain.Flights.Views
{
    public class FlightSummary
    {
        public FlightSummary(Guid id, DateTimeOffset createdAt, DateTimeOffset modifiedAt, string departureCity, string arrivalCity, DateTimeOffset departingAt, Configuration configuration, FlightBookingSummary bookingSummary)
        {
            Id = id;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            DepartureCity = departureCity ?? throw new ArgumentNullException(nameof(departureCity));
            ArrivalCity = arrivalCity ?? throw new ArgumentNullException(nameof(arrivalCity));
            DepartingAt = departingAt;
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            BookingSummary = bookingSummary ?? throw new ArgumentNullException(nameof(bookingSummary));
        }

        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset ModifiedAt { get; }
        public string DepartureCity { get; }
        public string ArrivalCity { get; }
        public DateTimeOffset DepartingAt { get; }
        public Configuration Configuration { get; }
        public FlightBookingSummary BookingSummary { get; }
    }
}
