using DddEfSample.Domain.Flights;
using DddEfSample.Domain.Flights.Views;
using DddEfSample.Web.ActionResults;
using DddEfSample.Web.Mapping;
using DddEfSample.Web.Models.Flights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DddEfSample.Web
{
    [Route("api/flights")]
    public class FlightController : Controller
    {
        private readonly IFlightView _view;
        private readonly IFlightRepository _repository;

        public FlightController(IFlightView view, IFlightRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("", Name = "GetAllFlights")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _view.GetAllAsync();
            return Ok(flights);
        }

        [Route("{id:Guid}", Name = "GetFlightById")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id, [FromHeader(Name = "If-None-Match")] string eTag = null)
        {
            var flight = await _view.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            if (eTag != null && flight.ETag == eTag)
            {
                return StatusCode(304); //Not Modified
            }
            
            return Ok(flight).WithHeader("ETag", flight.ETag);
        }

        [Route("", Name = "CreateFlight")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = model.ToDomain();
            await _repository.CreateAsync(flight);

            var url = Url.RouteUrl("GetFlightById", new { id = flight.Id });
            var summary = await _view.GetByIdAsync(flight.Id);
            return Created(url, summary).WithHeader("ETag", summary.ETag);
        }

        [Route("{id:Guid}", Name = "UpdateFlight")]
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateModel model, [FromHeader(Name = "If-Match")] string eTag = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = await _repository.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            if (eTag != null && eTag != flight.ETag)
            {
                return StatusCode(412); //Precondition Failed
            }

            var newConfiguration = model.Configuration.ToDomain();
            var result = flight.UpdateConfiguration(newConfiguration);
            if (!result.IsSuccess)
            {
                return result.Error.ToResult();
            }

            var updateResult = await _repository.UpdateAsync(flight);
            if (!updateResult.IsSuccess)
            {
                return updateResult.Error.ToResult();
            }

            var summary = await _view.GetByIdAsync(flight.Id);
            return Ok(summary).WithHeader("ETag", summary.ETag);
        }
    }
}
