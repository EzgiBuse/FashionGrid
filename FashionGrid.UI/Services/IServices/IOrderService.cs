using FashionGrid.UI.Models.Dtos;
using System.Threading.Tasks;

namespace FashionGrid.UI.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrderAsync(OrderDto order);
        Task<ResponseDto?> GetOrderByIdAsync(string id);
        Task<ResponseDto?> GetOrdersByUserIdAsync(string userId);
        Task<ResponseDto?> UpdateOrderStatusAsync(string orderId, OrderStatus status);
        Task<ResponseDto?> GetOrdersByDealerIdAsync(string dealerId);
        Task<ResponseDto?> GetDealerPanelIndexStatistics(string dealerId);
    }
}
