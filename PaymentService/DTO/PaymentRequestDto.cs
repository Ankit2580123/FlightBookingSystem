namespace PaymentService.DTO
{
    public class PaymentRequestDto
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
