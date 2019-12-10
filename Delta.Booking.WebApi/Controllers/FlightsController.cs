
using Delta.Booking.WebApi.Model;
using Delta.Booking.WebApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Delta.Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightRepository _repository;

        public FlightsController(IFlightRepository repository, ILogger<FlightsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpPost]
        public ActionResult Post([FromBody] FlightSearchModel flightSearch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            //Returns a Collection of <Departing, Returning> Flights
            var result = _repository.GetFlights(flightSearch);

            return Ok(result);
        }

    }


}
