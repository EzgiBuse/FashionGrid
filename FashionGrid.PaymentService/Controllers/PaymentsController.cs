using FashionGrid.PaymentService.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace FashionGrid.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        // Payment Controller in the Payment Service
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] List<CartItemDto> cartItems)
        {
            //var sessionUrl = await _paymentService.CreateStripeCheckoutSession(cartItems);
            return Ok(new { url = sessionUrl });
        }

        // Method in PaymentService to create Stripe session
        public async Task<string> CreateStripeCheckoutSession(List<CartItemDto> cartItems)
        {
            var options = new SessionCreateOptions
            {
                // Setup payment methods, line items from cartItems, and redirection URLs
                SuccessUrl = "https://example.com/payment-success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://example.com/payment-cancelled",
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session.Url; // URL to which the user will be redirected to complete the payment
        }

    }
}
