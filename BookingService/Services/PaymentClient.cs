using BookingService.DTO;

namespace BookingService.Services
{
    public class PaymentClient
    {
        private readonly HttpClient _httpClient;

        public PaymentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ProcessPayment(int bookingId, decimal amount)
        {
            var request = new PaymentRequestDto
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = "UPI"
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7290/api/payments",
                request
            );

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var payment = await response.Content.ReadFromJsonAsync<PaymentResponseDto>();
            return payment?.Status == "Success";
        }
    }
}
