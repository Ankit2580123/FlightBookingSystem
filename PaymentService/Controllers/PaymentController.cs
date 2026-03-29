using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentServices _paymentService;
        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(Payment payment)
        {
            var result = await _paymentService.ProcessPayment(payment);

            if (result.Status == "Success")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
