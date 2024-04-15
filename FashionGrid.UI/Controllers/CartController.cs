using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FashionGrid.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                
               var response = await _cartService.GetCartByUserIdAsync(userId);
                if (!response.IsSuccess)
                {
                    // Handle the case where the cart is not found or another error occurred
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                var cart = response.Result as CartDto; // Make sure the Result is cast to the type Cart
                if (cart == null)
                {
                    return NotFound("Cart not found.");
                }

                return View(cart);
            }
            catch (Exception ex)
            {
                // Log the error message or handle it according to your needs
                return Json(new { success = false, message = ex.Message });
            }
        }

      
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            //create cartitem
            try
            {
                string userId =  User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                // string userId1 = User.Identity.Name;
                request.Quantity = 1;


               await _cartService.AddToCartAsync(request,userId);

                // Simulate a successful add to cart
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }
            catch (Exception ex)
            {
                // Log the error message or handle it according to your needs
                return Json(new { success = false, message = ex.Message });
            }

        }

    }
}
