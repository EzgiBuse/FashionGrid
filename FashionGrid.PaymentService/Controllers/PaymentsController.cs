using FashionGrid.PaymentService.Models.Dtos;
using FashionGrid.PaymentService.Services;
using FashionGrid.PaymentService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace FashionGrid.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IStripePaymentService _stripePaymentService;

        public PaymentsController(IStripePaymentService stripePaymentService)
        {
            _stripePaymentService = stripePaymentService;
        }

        //[HttpPost("CreateCheckoutSession")]
        //public async Task<IActionResult> CreateCheckoutSession([FromBody] List<CartItemDto> items)
        //{
        //    try
        //    {
        //        var sessionUrl = await _stripePaymentService.CreateCheckoutSessionAsync(items);
        //        return Ok(new { Url = sessionUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost("CreateCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] decimal carttotal)
        {
            try
            {
                var sessionUrl = await _stripePaymentService.CreateCheckoutSessionAsync(carttotal);
                return Ok(new { Url = sessionUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
