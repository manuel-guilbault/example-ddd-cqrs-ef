using DddEfSample.Domain.Flights;
using DddEfSample.Web.Models.Flights;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Web.Mapping
{
    public static class FlightMappingExtensions
    {
        public static Flight ToDomain(this CreateModel model)
            => new Flight(
                model.Routing.ToDomain(),
                model.Schedule.ToDomain(),
                model.Configuration.ToDomain());

        public static Routing ToDomain(this RoutingModel model)
            => new Routing(model.DepartureCity, model.ArrivalCity);

        public static Schedule ToDomain(this ScheduleModel model)
            => new Schedule(model.CheckInAt, model.DepartureAt, model.ArrivalAt);

        public static Configuration ToDomain(this IEnumerable<PhysicalClassCapacityModel> physicalClassCapacities)
            => new Configuration(physicalClassCapacities.Select(x => new PhysicalClassCapacity(x.PhysicalClass, x.Capacity)));

        public static IActionResult ToResult(this Flight.ConfigurationError error)
        {
            switch (error)
            {
                case Flight.ConfigurationError.WouldCauseOverbooking:
                    return new StatusCodeResult(409); //Conflict
                default:
                    return new BadRequestResult();
            }
        }

        public static IActionResult ToResult(this Flight.BookingError error)
        {
            switch (error)
            {
                case Flight.BookingError.NoMoreCapacity:
                    return new StatusCodeResult(409); //Conflict
                default:
                    return new StatusCodeResult(500); //Internal Server Error
            }
        }

        public static IActionResult ToResult(this FlightUpdateError error)
        {
            switch (error)
            {
                case FlightUpdateError.NotFound:
                case FlightUpdateError.ConcurrencyConflict:
                    return new StatusCodeResult(409); //Conflict
                default:
                    return new StatusCodeResult(500); //Internal Server Error
            }
        }
    }
}
