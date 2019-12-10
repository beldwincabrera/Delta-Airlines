using Delta.Booking.WebApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Delta.Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightRepository _repository;

        public AirportsController(IFlightRepository repository, ILogger<FlightsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Get(string code)
        {

            if (string.IsNullOrWhiteSpace(code))
            {
                return NotFound();
            }

            var airports = _repository.GetAirports(code);

            return Ok(airports);
        }

    }
}
