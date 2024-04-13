using FashionGrid.OrderService.Models;
using FashionGrid.OrderService.Models.Dto;
using FashionGrid.OrderService.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FashionGrid.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ResponseDto _responseDto;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrder(order);
                _responseDto.Result = createdOrder;
                _responseDto.Message = "Order created successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = $"Error creating the order: {ex.Message}";
                return BadRequest(_responseDto);
            }
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, _responseDto);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Order not found";
                    return NotFound(_responseDto);
                }
                _responseDto.Result = order;
                _responseDto.Message = "Order retrieved successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = $"Error retrieving the order: {ex.Message}";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("GetOrdersByUserId/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserId(userId);
                _responseDto.Result = orders;
                _responseDto.Message = "Orders retrieved successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = $"Error retrieving orders: {ex.Message}";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut("{orderId}/{status}")]
        public async Task<IActionResult> UpdateOrderStatus(string orderId,OrderStatus status)
        {
            try
            {
                await _orderService.UpdateOrderStatus(orderId, status);
                _responseDto.Message = "Order status updated successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = $"Error updating order status: {ex.Message}";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("GetOrdersByDealerId/{dealerId}")]
        public async Task<IActionResult> GetOrdersByDealerId(string dealerId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByDealerId(dealerId);
                _responseDto.Result = orders;
                _responseDto.Message = "Orders retrieved successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = $"Error retrieving orders: {ex.Message}";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
