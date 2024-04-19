using FashionGrid.UI.Models;
using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateOrderAsync(OrderDto order)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = order,
                Url = "https://localhost:7203/api/Orders"
            });
        }

        public async Task<ResponseDto?> GetOrderByIdAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7203/api/Orders/GetOrderById/{id}"
            });
        }

        public async Task<ResponseDto?> GetOrdersByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7203/api/Orders/GetOrdersByUserId/{userId}"
            });
        }

        public async Task<ResponseDto?> UpdateOrderStatusAsync(string orderId, OrderStatus status)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Url = $"https://localhost:7203/api/Orders/{orderId}/{status}"
            });
        }

        public async Task<ResponseDto?> GetOrdersByDealerIdAsync(string dealerId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7203/api/Orders/GetOrdersByDealerId/{dealerId}"
            });
        }
    }
}
