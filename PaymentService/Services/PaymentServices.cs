using PaymentService.Models;

namespace PaymentService.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly PaymentDbContext _context;

        public PaymentServices(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcessPayment(Payment payment)
        {
            if (payment.Amount <= 0)
            {
                payment.Status = "Failed";
                payment.TransactionId = Guid.NewGuid().ToString();
                payment.CreatedAt = DateTime.UtcNow;

                await SavePayment(payment);
                return payment;
            }

            // 2. Simulate Processing Delay (like real gateway)
            await Task.Delay(1000);

            var random = new Random();
            int chance = random.Next(1, 101); // 1–100

            switch (payment.PaymentMethod?.ToLower())
            {
                case "upi":
                    payment.Status = chance <= 90 ? "Success" : "Failed"; // 90% success
                    break;

                case "card":
                    payment.Status = chance <= 80 ? "Success" : "Failed"; // 80% success
                    break;

                case "netbanking":
                    payment.Status = chance <= 85 ? "Success" : "Failed";
                    break;

                default:
                    payment.Status = "Failed"; // unknown method
                    break;
            }

            // 4. Simulate Random Failure Reason
            HandlePaymentFailure(payment, random);

            // 5. Generate Transaction Details
            payment.TransactionId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            payment.CreatedAt = DateTime.UtcNow;

            await SavePayment(payment);
            return payment;
        }

        private static void HandlePaymentFailure(Payment payment, Random random)
        {
            try
            {

                if (payment.Status == "Failed")
                {
                    var failureReasons = new[]
                     {
                    "Insufficient Balance",
                    "Bank Server Down",
                    "Payment Timeout",
                    "Invalid Details"
                };

                    var reason = failureReasons[random.Next(failureReasons.Length)];

                    // (Optional) You can store this later in DB column
                    Console.WriteLine($"Payment Failed: {reason}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Helper Method (clean code)
        private async Task SavePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
