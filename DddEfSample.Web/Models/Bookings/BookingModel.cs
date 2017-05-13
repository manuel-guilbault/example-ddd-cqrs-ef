using DddEfSample.Domain.Flights;
using System;
using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Bookings
{
    public class BookingModel
    {
        [Required]
        public Guid FlightId { get; set; }

        [Required]
        public PhysicalClassIataCode PhysicalClass { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfSeats { get; set; }
    }
}
