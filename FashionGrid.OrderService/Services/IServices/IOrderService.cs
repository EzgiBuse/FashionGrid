using FashionGrid.OrderService.Models;

namespace FashionGrid.OrderService.Services.IServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task UpdateOrderStatus(string orderId, OrderStatus status);
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersByDealerId(string dealerId); 
       Task<Order> GetOrderById(string orderId);
    }

}
