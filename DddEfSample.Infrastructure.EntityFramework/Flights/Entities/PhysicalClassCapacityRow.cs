using DddEfSample.Domain.Flights;
using System;

namespace DddEfSample.Infrastructure.EntityFramework.Flights.Entities
{
    public class PhysicalClassCapacityRow
    {
        public Guid FlightId { get; set; }
        public PhysicalClassIataCode PhysicalClass { get; set; }
        public int Capacity { get; set; }
    }
}
