using Microsoft.AspNetCore.Mvc;
using SneakerSZN.RequestModels;
using Stripe;

namespace SneakerSZN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentIntentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentIntentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentRequest paymentRequest)
        {
            var secretKey = _configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = secretKey;

            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(paymentRequest.Amount * 100),  // Convert EUR to cents
                    Currency = "eur",  // You can change this to the correct currency
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = paymentRequest.Description,
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
