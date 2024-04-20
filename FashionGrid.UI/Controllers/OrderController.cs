using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace FashionGrid.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, ICartService cartService, IProductService productService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var isSuccess = await OrderConfirmation();
            return View("OrderConfirmation");
        }

        public async Task<bool> OrderConfirmation()
        {
            try
            {
                //Creating an Order For the User
                string userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                // Fetch the user's cart
                var cartResult = await _cartService.GetCartByUserIdAsync(userId);
                if (!cartResult.IsSuccess || cartResult.Result == null)
                {
                    return false;
                }

               
                var cart = JsonConvert.DeserializeObject<CartResponseDto>(cartResult.Result.ToString());
                var cartTotal = cart.Result.TotalPrice;
                var userCartItems = cart.Result.Items;
                OrderDto order = new OrderDto
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    Status = 0,
                    OrderItems = new List<OrderItemDto>(),
                    TotalAmount = cart.Result.TotalPrice,
                    PaymentDetails = new PaymentDetailsDto
                    {
                        StripeSessionId = "sess_xyz123",  
                        PaymentIntentId = "pi_xyz123",    
                        PaymentStatus = PaymentStatus.Paid
                    }

            };

                // Populate order items
                foreach (var item in userCartItems)
                {
                    // Retrieve product details including Dealer
                    var productResult = await _productService.GetProductByIdAsync(item.ProductId);
                    if (!productResult.IsSuccess)
                    {
                        continue; 
                    }

                    var productDetails = JsonConvert.DeserializeObject<ProductResponseDto>(productResult.Result.ToString());

                    order.OrderItems.Add(new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Name,
                        DealerId = productDetails.Result.UserId, //  DealerId
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                // Create the order via OrderService
                var orderResult = await _orderService.CreateOrderAsync(order);
                if (orderResult.IsSuccess)
                {
                    await _cartService.ClearCartAsync(userId); //Clearing the user's cart after the order has been created
                    return true ;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
