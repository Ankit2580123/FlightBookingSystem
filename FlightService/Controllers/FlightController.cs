using FlightService.DTO;
using FlightService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [ApiController]
    [Route("api/flight")]
    public class FlightController : ControllerBase
    {

        private readonly IFlightServices _service;

        public FlightController(IFlightServices service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(FlightDto dto)
        {
            var result = await _service.AddFlight(dto);
            return Ok(result);
        }

        [HttpGet("search")]
        public IActionResult Search(string source, string destination)
        {

            if (string.IsNullOrWhiteSpace(source))
            {
                return BadRequest("Source is mandatorys.");
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                return BadRequest("Destination is mandatory.");
            }

            var flights = _service.Search(source, destination);
            return Ok(flights);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var flight = await _service.GetById(id);
            if (flight == null) return NotFound();

            return Ok(flight);
        }

        [HttpPost("reduce-seats")]
        public async Task<IActionResult> ReduceSeats([FromBody] ReduceSeatsDto dto)
        {
            var success = await _service.ReduceSeats(dto.FlightId, dto.Seats);

            if (!success)
                return BadRequest(new { message = "Flight not found or not enough seats" });

            return Ok(new { message = "Seats updated" });
        }

        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception!");
        }

        [HttpPost("increase-seats")]
        public async Task<IActionResult> IncreaseSeats([FromBody] IncreaseSeatsDto dto)
        {
            var result = await _service.IncreaseSeats(dto.FlightId, dto.Count);

            if (!result)
                return NotFound();

            return Ok(true);
        }
    }
}       
   
