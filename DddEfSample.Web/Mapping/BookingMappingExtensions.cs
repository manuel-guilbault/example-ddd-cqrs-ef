using DddEfSample.Domain.Flights;
using DddEfSample.Web.Models.Bookings;

namespace DddEfSample.Web.Mapping
{
    public static class BookingMappingExtensions
    {
        public static Booking ToDomain(this BookingModel model)
            => new Booking(model.PhysicalClass, model.NumberOfSeats);
    }
}
