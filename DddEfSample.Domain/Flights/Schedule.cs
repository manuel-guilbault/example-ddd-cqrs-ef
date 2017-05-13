using System;

namespace DddEfSample.Domain.Flights
{
    public class Schedule
    {
        public Schedule(DateTimeOffset checkInAt, DateTimeOffset departureAt, DateTimeOffset arrivalAt)
        {
            CheckInAt = checkInAt;
            DepartureAt = departureAt;
            ArrivalAt = arrivalAt;
        }

        public DateTimeOffset CheckInAt { get; }
        public DateTimeOffset DepartureAt { get; }
        public DateTimeOffset ArrivalAt { get; }
    }
}
