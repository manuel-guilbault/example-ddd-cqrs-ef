using DddEfSample.Domain.Flights;
using DddEfSample.Domain.Flights.Views;
using DddEfSample.Web.Mapping;
using DddEfSample.Web.Models.Bookings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DddEfSample.Web.Controllers
{
    [Route("api/bookings")]
    public class BookingController: Controller
    {
        private readonly IBookingView _view;
        private readonly IFlightRepository _repository;

        public BookingController(IBookingView view, IFlightRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("", Name = "GetAllBookings")]
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid flightId)
        {
            var bookings = await _view.GetAllForFlightAsync(flightId);
            return Ok(bookings);
        }

        [Route("{id:Guid}", Name = "GetBookingById")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _view.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [Route("", Name = "CreateBooking")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = await _repository.GetByIdAsync(model.FlightId);
            if (flight == null)
            {
                return NotFound();
            }

            var bookingId = Guid.NewGuid();
            var result = flight.Book(bookingId, model.PhysicalClass, model.NumberOfSeats);
            if (!result.IsSuccess)
            {
                return result.Error.ToResult();
            }
            
            var updateResult = await _repository.UpdateAsync(flight);
            if (!updateResult.IsSuccess)
            {
                return updateResult.Error.ToResult();
            }

            var url = Url.RouteUrl("GetBookingById", new { id = bookingId });
            var booking = await _view.GetByIdAsync(bookingId);
            return Created(url, booking);
        }
    }
}
