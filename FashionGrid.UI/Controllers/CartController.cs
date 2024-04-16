using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                if (response == null || !response.IsSuccess)
                {
                   
                    return View(new CartDto());
                }

               
                var cart = JsonConvert.DeserializeObject<CartResponseDto>(response.Result.ToString());
                var cartDto = cart.Result;
                if (cart == null)
                {
                    return View(new CartDto()) ;
                }

                return View(cartDto);
            }
            catch (Exception ex)
            {
               
                return View(new CartDto()) ;
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

        
        public async Task<IActionResult> RemoveItem(string cartItemId)
        {
            try
            {
                string userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
                return RedirectToAction("Index"); // Redirect to the cart view or wherever appropriate
            }
            catch (Exception ex)
            {
                
                return View();
            }
        }


    }
}
