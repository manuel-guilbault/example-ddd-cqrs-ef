using DddEfSample.Domain.Flights;
using DddEfSample.Domain.Flights.Views;
using DddEfSample.Web.Models.Flights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DddEfSample.Web
{
    [Route("api/Flights")]
    public class FlightController : Controller
    {
        private readonly IFlightSummaryView _view;
        private readonly IFlightRepository _repository;

        public FlightController(IFlightSummaryView view, IFlightRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("", Name = "GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _view.GetAll();
            return Ok(flights);
        }

        [Route("{id:Guid}", Name = "GetById")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var flight = await _view.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("", Name = "Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Properties properties)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = new Flight(
                properties.DepartureCity,
                properties.ArrivalCity,
                properties.DepartingAt,
                new Configuration(
                    properties.Configuration.Select(x => new Domain.Flights.PhysicalClassCapacity(x.PhysicalClass, x.Capacity))));
            await _repository.CreateAsync(flight);

            var summary = await _view.GetById(flight.Id);
            return Created(Url.RouteUrl("GetById", new { id = flight.Id }), summary);
        }
    }
}
