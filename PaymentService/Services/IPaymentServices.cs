using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IPaymentServices
    {
        Task<Payment> ProcessPayment(Payment payment);
    }
}
