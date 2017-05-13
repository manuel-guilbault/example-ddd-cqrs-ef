using System;
using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Flights
{
    public class ScheduleModel
    {
        [Required]
        public DateTimeOffset CheckInAt { get; set; }

        [Required]
        public DateTimeOffset DepartureAt { get; set; }

        [Required]
        public DateTimeOffset ArrivalAt { get; set; }
    }
}
