using BookingService.DTO;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Services
{
    public class BookingServices:IBookingService
    {
        private readonly BookingDbContext _context;
        private readonly FlightClient _flightClient;
        private readonly PaymentClient _paymentClient;


        public BookingServices(BookingDbContext context, FlightClient flightClient,PaymentClient paymentClient)
        {
            _context = context;
            _flightClient = flightClient;
            _paymentClient = paymentClient;
        }
        public async Task<string> CreateBooking(CreateBookingDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                int seatCount = dto.Passengers.Count;

                var success = await _flightClient.ReduceSeats(dto.FlightId, seatCount);
                if (!success)
                    return "Not enough seats";

                var booking = new Booking
                {
                    UserId = dto.UserId,
                    FlightId = dto.FlightId,
                    Seats = seatCount,
                    TotalAmount = seatCount * 5000,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                // Add history

                var bookingHistory = new BookingStatusHistory
                {
                    BookingId = booking.Id,
                    Status = "Pending",
                    UpdatedAt = DateTime.UtcNow
                };


                var paymentSuccess = await _paymentClient.ProcessPayment(booking.Id, booking.TotalAmount);

                if (!paymentSuccess)
                {
                    await _flightClient.IncreaseSeats(dto.FlightId, seatCount);
                    throw new Exception("Payment Failed");
                }

                booking.Status = "Confirmed";
                bookingHistory.Status = "Confirmed";


                foreach (var p in dto.Passengers)
                {
                    _context.BookingPassengers.Add(new BookingPassenger
                    {
                        BookingId = booking.Id,
                        PassengerName = p.PassengerName,
                        Age = p.Age,
                        Gender = p.Gender
                    });
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Booking Confirmed";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Booking Failed";
            }
        }
        public async Task<List<Booking>> GetAllBookings()
        {
            try
            {
                var bookings = await _context.Bookings.ToListAsync();
                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookinsgs", ex);
            }
        }
    }
}
