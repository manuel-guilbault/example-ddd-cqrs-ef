using System;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Entities
{
    public class ScheduleValues
    {
        public DateTimeOffset CheckInAt { get; set; }
        public DateTimeOffset DepartureAt { get; set; }
        public DateTimeOffset ArrivalAt { get; set; }
    }
}
