using DddEfSample.Domain.Flights;
using DddEfSample.Domain.Flights.Views;
using DddEfSample.Infrastructure.EntityFramework.Flights.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Infrastructure.EntityFramework.Flights
{
    public static class MappingExtensions
    {
        public static Flight ToDomain(this FlightRow row)
        {
            return new Flight(
                row.Id,
                row.RowVersion.ToETag(),
                row.Routing.ToDomain(),
                row.Schedule.ToDomain(),
                row.PhysicalClassCapacities.ToDomain(),
                row.Bookings.ToDomain()
            );
        }

        public static Routing ToDomain(this RoutingValues values)
            => new Routing(values.DepartureCity, values.ArrivalCity);

        public static Schedule ToDomain(this ScheduleValues values)
            => new Schedule(values.CheckInAt, values.DepartureAt, values.ArrivalAt);

        public static Configuration ToDomain(this IEnumerable<PhysicalClassCapacityRow> rows)
            => new Configuration(rows.Select(x => new PhysicalClassCapacity(x.PhysicalClass, x.Capacity)));

        public static IEnumerable<Booking> ToDomain(this IEnumerable<BookingRow> rows)
            => rows.Select(x => new Booking(x.Id, x.BookedAt, x.PhysicalClass, x.NumberOfSeats));

        public static void MapTo(this Flight flight, FlightRow row)
        {
            row.Id = flight.Id;
            row.Routing = flight.Routing.ToEntity();
            row.Schedule = flight.Schedule.ToEntity();
            row.PhysicalClassCapacities.SynchronizeWith(
                flight.Configuration,
                r => r.PhysicalClass,
                d => d.PhysicalClass,
                (r, d) => d.MapTo(r, flight));
            row.Bookings.SynchronizeWith(
                flight.Bookings, 
                r => r.Id, 
                d => d.Id, 
                (r, d) => d.MapTo(r, flight));
        }

        public static RoutingValues ToEntity(this Routing routing)
        {
            return new RoutingValues()
            {
                DepartureCity = routing.DepartureCity,
                ArrivalCity = routing.ArrivalCity,
            };
        }

        public static ScheduleValues ToEntity(this Schedule schedule)
        {
            return new ScheduleValues()
            {
                CheckInAt = schedule.CheckInAt,
                DepartureAt = schedule.DepartureAt,
                ArrivalAt = schedule.ArrivalAt,
            };
        }

        public static void MapTo(this PhysicalClassCapacity physicalClassCapacity, PhysicalClassCapacityRow row, Flight flight)
        {
            row.FlightId = flight.Id;
            row.PhysicalClass = physicalClassCapacity.PhysicalClass;
            row.Capacity = physicalClassCapacity.Capacity;
        }

        public static void MapTo(this Booking booking, BookingRow row, Flight flight)
        {
            row.Id = booking.Id;
            row.FlightId = flight.Id;
            row.BookedAt = booking.BookedAt;
            row.PhysicalClass = booking.PhysicalClass;
            row.NumberOfSeats = booking.NumberOfSeats;
        }
    }
}
