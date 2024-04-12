using FashionGrid.CartService.Models;
using FashionGrid.CartService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        // GET: api/cart/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(string id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: api/cart/{cartId}/item
        [HttpPost("{cartId}/item")]
        public async Task<IActionResult> AddItemToCart(string cartId, [FromBody] CartItem item)
        {
            await _cartService.AddItemToCartAsync(cartId, item);
            return CreatedAtAction(nameof(GetCartById), new { id = cartId }, item);
        }

        // PUT: api/cart/{cartId}/item/{productId}
        [HttpPut("{cartId}/item/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(string cartId, string productId, [FromBody] int quantity)
        {
            await _cartService.UpdateItemQuantityAsync(cartId, productId, quantity);
            return NoContent();
        }

        // DELETE: api/cart/{cartId}/item/{productId}
        [HttpDelete("{cartId}/item/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(string cartId, string productId)
        {
            await _cartService.RemoveItemFromCartAsync(cartId, productId);
            return NoContent();
        }

        // DELETE: api/cart/{cartId}/clear
        [HttpDelete("{cartId}/clear")]
        public async Task<IActionResult> ClearCart(string cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return NoContent();
        }
    }
}
