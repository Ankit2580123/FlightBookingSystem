using Microsoft.EntityFrameworkCore;

namespace BookingService.Services
{
    public class FlightClient
    {
        private readonly HttpClient _httpClient;

        public FlightClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReduceSeats(int flightId, int seats)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7218/api/flight/reduce-seats",
                new
                {
                    flightId = flightId,
                    seats = seats
                });

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> IncreaseSeats(int flightId, int count)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7218/api/flight/increase-seats",
                new
                {
                    FlightId = flightId,
                    Count = count
                });

            return response.IsSuccessStatusCode;
        }

    }
}
