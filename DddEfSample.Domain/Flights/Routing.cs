using System;

namespace DddEfSample.Domain.Flights
{
    public class Routing
    {
        public Routing(string departureCity, string arrivalCity)
        {
            DepartureCity = departureCity ?? throw new ArgumentNullException(nameof(departureCity));
            ArrivalCity = arrivalCity ?? throw new ArgumentNullException(nameof(arrivalCity));
        }

        public string DepartureCity { get; }
        public string ArrivalCity { get; }
    }
}
