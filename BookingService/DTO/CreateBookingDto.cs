namespace BookingService.DTO
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public List<PassengerDto> Passengers { get; set; }
    }
}
