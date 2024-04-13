using FashionGrid.CartService.Models;
using FashionGrid.CartService.Models.Dto;
using FashionGrid.CartService.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FashionGrid.CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ResponseDto _responseDto;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
            _responseDto = new ResponseDto();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(userId);
                _responseDto.Result = cart;
                _responseDto.Message = "Cart retrieved successfully.";
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddItemToCart(string userId, [FromBody] CartItem item)
        {
            try
            {
                await _cartService.AddItemToCartAsync(userId, item);
                _responseDto.Message = "Item added to cart successfully.";
                return CreatedAtAction(nameof(GetCartByUserId), new { userId = userId }, _responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{userId}/{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(string userId, string cartItemId)
        {
            try
            {
                await _cartService.DeleteCartItemAsync(userId, cartItemId);
                _responseDto.Message = "Item removed from cart successfully.";
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut("{userId}/{cartItemId}/{quantity}")]
        public async Task<IActionResult> UpdateCartItem(string userId, string cartItemId,int quantity)
        {
            try
            {
                await _cartService.UpdateCartItemAsync(userId, cartItemId, quantity);
                _responseDto.Message = "Cart item updated successfully.";
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            try
            {
                await _cartService.ClearCartAsync(userId);
                _responseDto.Message = "Cart cleared successfully.";
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }
    }
}
