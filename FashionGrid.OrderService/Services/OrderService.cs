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
            var databaseName = configuration["MongoDb:DatabaseName"];
            var collectionName = configuration["MongoDb:OrderCollectionName"];

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

        public async Task<DealerPanelIndexModel> GetDealerPanelIndexStatistics(string dealerId)
        {
            var today = DateTime.UtcNow;
            var startOfDay = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, DateTimeKind.Utc);
            var startOfWeek = startOfDay.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            // Filter orders by dealer ID and by date ranges for daily, weekly, and monthly orders
            var dailyFilter = Builders<Order>.Filter.Eq(order => order.OrderItems[0].DealerId, dealerId) &
                              Builders<Order>.Filter.Gte(order => order.OrderDate, startOfDay);
            var weeklyFilter = Builders<Order>.Filter.Eq(order => order.OrderItems[0].DealerId, dealerId) &
                               Builders<Order>.Filter.Gte(order => order.OrderDate, startOfWeek);
            var monthlyFilter = Builders<Order>.Filter.Eq(order => order.OrderItems[0].DealerId, dealerId) &
                                Builders<Order>.Filter.Gte(order => order.OrderDate, startOfMonth);

            // Get the total number of orders and total sales amount for the dealer in the specified date ranges
            var dailyOrders = await _orders.Find(dailyFilter).ToListAsync();
            var weeklyOrders = await _orders.Find(weeklyFilter).ToListAsync();
            var monthlyOrders = await _orders.Find(monthlyFilter).ToListAsync();
            var allOrders = await _orders.Find(Builders<Order>.Filter.Eq(order => order.OrderItems[0].DealerId, dealerId)).ToListAsync();

            // Compute the statistics
            var viewModel = new DealerPanelIndexModel
            {
                OrderCountDaily = dailyOrders.Count,
                OrderTotalDaily = dailyOrders.Sum(order => order.TotalAmount),
                OrderCountWeekly = weeklyOrders.Count,
                OrderTotalWeekly = weeklyOrders.Sum(order => order.TotalAmount),
                OrderCountMonthly = monthlyOrders.Count,
                OrderTotalMonthly = monthlyOrders.Sum(order => order.TotalAmount),
                TotalCustomerCount = allOrders.Select(order => order.UserId).Distinct().Count(),
                OrderCountTotal = allOrders.Count,
                OrderTotal = allOrders.Sum(order => order.TotalAmount),
               
                TopProductName = allOrders.SelectMany(order => order.OrderItems)
                                          .GroupBy(item => item.ProductName)
                                          .OrderByDescending(group => group.Sum(item => item.Quantity))
                                          .FirstOrDefault()?.Key
            };

            return viewModel;
        }

    }
}
