using BookingService.DTO;
using BookingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            try
            {
                // Validate input
                if (dto == null)
                {
                    return BadRequest(new { message = "Booking request cannot be null" });
                }

                if (dto.Passengers == null || dto.Passengers.Count == 0)
                {
                    return BadRequest(new { message = "At least one passenger is required" });
                }

                if (dto.UserId <= 0 || dto.FlightId <= 0)
                {
                    return BadRequest(new { message = "Invalid UserId or FlightId" });
                }

                var result = await _service.CreateBooking(dto);

                if (result == "Booking Confirmed")
                {
                    return Ok(new { message = "Booking created successfully", status = result });
                }
                else if (result == "Not enough seats")
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { message = "Not enough seats available for the flight" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new { message = result });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Invalid argument provided", error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                    new { message = "Operation could not be completed", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while creating the bookings", error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings = await _service.GetAllBookings();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error retrieving bookingss", error = ex.Message });
            }
        }
    }
}
