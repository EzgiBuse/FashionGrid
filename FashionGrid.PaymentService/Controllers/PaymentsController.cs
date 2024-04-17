using FashionGrid.PaymentService.Models.Dtos;
using FashionGrid.PaymentService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace FashionGrid.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly StripePaymentService _stripePaymentService;

        public PaymentsController(StripePaymentService stripePaymentService)
        {
            _stripePaymentService = stripePaymentService;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] List<CartItemDto> items)
        {
            try
            {
                var sessionUrl = await _stripePaymentService.CreateCheckoutSessionAsync(items);
                return Ok(new { Url = sessionUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
