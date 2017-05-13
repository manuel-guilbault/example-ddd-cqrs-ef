using System.ComponentModel.DataAnnotations;

namespace DddEfSample.Web.Models.Flights
{
    public class CreationModel
    {
        [Required]
        public RoutingModel Routing { get; set; }

        [Required]
        public ScheduleModel Schedule { get; set; }

        [Required]
        public ConfigurationModel Configuration { get; set; }
    }
}
