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

        
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var carts = await _cartService.GetAllCartsAsync();
                _responseDto.Result = carts;
                _responseDto.Message = "Retrieved all carts successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("GetCartById/{id}")]
        public async Task<IActionResult> GetCartById(string id)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(id);
                if (cart == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Cart not found.";
                    return NotFound(_responseDto);
                }
                _responseDto.Result = cart;
                _responseDto.Message = "Cart retrieved successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        
        [HttpPost("AddItemToCartByUserId/{userId}")]
        public async Task<IActionResult> AddItemToCartByUserId(string userId, [FromBody] CartItem item)
        {
            try
            {
                await _cartService.AddItemToCartByUserIdAsync(userId, item);
                _responseDto.Result = item;
                _responseDto.Message = "Item added to cart successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return CreatedAtAction(nameof(GetCartById), new { id = userId }, _responseDto);
        }

        [HttpPut("UpdateItemQuantity/{cartId}/{productId}/{quantity}")]
        public async Task<IActionResult> UpdateItemQuantity(string cartId, string productId, int quantity)
        {
            try
            {
                await _cartService.UpdateItemQuantityAsync(cartId, productId, quantity);
                _responseDto.Message = "Item quantity updated successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return NoContent();
        }

       
        [HttpDelete("RemoveItemFromCart/{cartId}/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(string cartId, string productId)
        {
            try
            {
                await _cartService.RemoveItemFromCartAsync(cartId, productId);
                _responseDto.Message = "Item removed from cart successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return NoContent();
        }

       
        [HttpDelete("ClearCart/{cartId}")]
        public async Task<IActionResult> ClearCart(string cartId)
        {
            try
            {
                await _cartService.ClearCartAsync(cartId);
                _responseDto.Message = "Cart cleared successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return NoContent();
        }
    }
}
