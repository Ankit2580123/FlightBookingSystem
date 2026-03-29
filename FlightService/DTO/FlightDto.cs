namespace FlightService.DTO
{
    public class FlightDto
    {
        public string FlightNumber { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public int TotalSeats { get; set; }
        public decimal Price { get; set; }
    }
}
