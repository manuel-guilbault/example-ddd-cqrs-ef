using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Flights
{
    public class RoutingModel
    {
        [Required]
        [MaxLength(100)]
        public string DepartureCity { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArrivalCity { get; set; }
    }
}
