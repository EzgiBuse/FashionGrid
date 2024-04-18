using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using static FashionGrid.UI.Models.Dtos.CheckoutResultUrlDto;

namespace FashionGrid.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ICartService _cartService;

        public PaymentController(IPaymentService paymentService,ICartService cartService)
        {
            _paymentService = paymentService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession()
        {//prepares cartItems before sending to payment controller for checkout
          
                try
                {
                    string userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                    // Fetch the user's cart
                    var cartResult = await _cartService.GetCartByUserIdAsync(userId);
                    if (!cartResult.IsSuccess || cartResult.Result == null)
                    {
                        return View(new CartDto());
                    }

                    // Assume cartDto contains the necessary cart data

                    var cart = JsonConvert.DeserializeObject<CartResponseDto>(cartResult.Result.ToString());
                    var total = cart.Result.TotalPrice;
                    var userCart = cart.Result.Items;

                    // Redirect to Stripe Checkout
                    var response = await _paymentService.CreateStripeSessionAsync(userCart);
                    var result = JsonConvert.DeserializeObject<CheckoutResultUrlDto>(response.Result.ToString());

                  return Redirect(result.Url);
                 }
                catch (System.Exception ex)
                {
                   
                    return View(new CartDto());
                }
            
        }
    }
}
