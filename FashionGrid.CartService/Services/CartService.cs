using FashionGrid.CartService.Models;
using FashionGrid.CartService.Services.IServices;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FashionGrid.CartService.Services
{
    public class CartService : ICartService
    {
        private readonly IMongoCollection<Cart> _carts;

        public CartService(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
            var collectionName = configuration.GetValue<string>("MongoDB:CollectionName");
            var database = mongoClient.GetDatabase(databaseName);
            _carts = database.GetCollection<Cart>(collectionName);
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            try
            {
                return await _carts.Find(cart => cart.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while retrieving the cart by UserId: {userId}", ex);
            }
        }

        public async Task AddItemToCartAsync(string userId, CartItem newItem)
        {
            try
            {
                var cart = await GetOrCreateCartAsync(userId);

                // Check if the cart already contains an item with the same productId and attributes
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == newItem.ProductId &&
                                                                  Enumerable.SequenceEqual(i.Attributes.OrderBy(a => a),
                                                                                           newItem.Attributes.OrderBy(a => a)));

                if (existingItem != null)
                {
                    // If found, update the quantity of the existing item
                    var filter = Builders<Cart>.Filter.And(
                        Builders<Cart>.Filter.Eq(c => c.UserId, userId),
                        Builders<Cart>.Filter.ElemMatch(c => c.Items, i => i.Id == existingItem.Id)
                    );
                    var update = Builders<Cart>.Update.Set("Items.$.Quantity", existingItem.Quantity + newItem.Quantity);
                    await _carts.UpdateOneAsync(filter, update);
                }
                else
                {
                    // If not found, add the new item to the cart
                    var update = Builders<Cart>.Update.Push(c => c.Items, newItem);
                    await _carts.UpdateOneAsync(c => c.UserId == userId, update);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding or updating an item in the cart for UserId: {userId}", ex);
            }
        }


        public async Task DeleteCartItemAsync(string userId, string cartItemId)
        {
            try
            {
                var update = Builders<Cart>.Update.PullFilter(cart => cart.Items, item => item.Id == cartItemId);
                await _carts.UpdateOneAsync(cart => cart.UserId == userId, update);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting an item from the cart for UserId: {userId}", ex);
            }
        }

        public async Task UpdateCartItemAsync(string userId, string cartItemId, int quantity)
        {
            try
            {
                var filter = Builders<Cart>.Filter.And(
                    Builders<Cart>.Filter.Eq(cart => cart.UserId, userId),
                    Builders<Cart>.Filter.ElemMatch(cart => cart.Items, item => item.Id == cartItemId)
                );
                var update = Builders<Cart>.Update.Set("Items.$.Quantity", quantity);
                await _carts.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the quantity of an item for UserId: {userId}", ex);
            }
        }

        public async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            try
            {
                var cart = await _carts.Find<Cart>(c => c.UserId == userId).FirstOrDefaultAsync();
                if (cart == null)
                {
                    cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                    await _carts.InsertOneAsync(cart);
                }
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving or creating a cart for UserId: {userId}", ex);
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            try
            {
                var update = Builders<Cart>.Update.Set(c => c.Items, new List<CartItem>());
                await _carts.UpdateOneAsync(c => c.UserId == userId, update);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while clearing the cart for UserId: {userId}", ex);
            }
        }



    }
}
