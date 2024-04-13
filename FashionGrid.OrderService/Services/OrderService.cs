using FashionGrid.OrderService.Models;
using FashionGrid.OrderService.Services.IServices;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionGrid.OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
            var collectionName = configuration.GetValue<string>("MongoDB:CollectionName");
            var database = mongoClient.GetDatabase(databaseName);
            _orders = database.GetCollection<Order>(collectionName);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                await _orders.InsertOneAsync(order);
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }

        public async Task UpdateOrderStatus(string orderId, OrderStatus status)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var update = Builders<Order>.Update.Set(o => o.Status, status);

            try
            {
                await _orders.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the order status for OrderId: {orderId}", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            try
            {
                return await _orders.Find(order => order.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving orders for UserId: {userId}", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                return await _orders.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all orders.", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByDealerId(string dealerId)
        {
            try
            {
                return await _orders.Find(order => order.OrderItems.Exists(item => item.DealerId == dealerId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving orders for DealerId: {dealerId}", ex);
            }
        }


        
        public async Task<Order> GetOrderById(string orderId)
        {
            try
            {
                var order = await _orders.Find(order => order.Id == orderId).FirstOrDefaultAsync();
                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {orderId} not found.");
                }
                return order;
            }
            catch (Exception ex)
            {
                // You can handle specific MongoDB exceptions if necessary or log them accordingly
                throw new Exception($"An error occurred while retrieving the order with ID: {orderId}.", ex);
            }
        }
    }
}
