using DddEfSample.Domain.Flights;
using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Flights
{
    public class PhysicalClassCapacity
    {
        public PhysicalClassIataCode PhysicalClass { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }
    }
}
