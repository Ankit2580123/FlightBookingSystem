using BookingService.DTO;
using BookingService.Models;

namespace BookingService.Services
{
    public interface IBookingService
    {
        Task<string> CreateBooking(CreateBookingDto dto);
        Task<List<Booking>> GetAllBookings();
    }
}
