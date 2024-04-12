
using FashionGrid.CartService.Models;
using FashionGrid.CartService.Services.IServices;
using MongoDB.Driver;

namespace FashionGrid.CartService.Services
{
   
        public class CartService :ICartService
        {
            private readonly IMongoCollection<Cart> _carts;

            public CartService(IMongoClient mongoClient,IConfiguration configuration)
            {
            var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
            var collectionName = configuration.GetValue<string>("MongoDB:CollectionName");
            var database = mongoClient.GetDatabase(databaseName);
            _carts = database.GetCollection<Cart>(collectionName);
            }

            public async Task<List<Cart>> GetAllCartsAsync()
            {
                return await _carts.Find(cart => true).ToListAsync();
            }

            public async Task<Cart> GetCartByIdAsync(string id)
            {
                return await _carts.Find<Cart>(cart => cart.Id == id).FirstOrDefaultAsync();
            }

        public async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _carts.Find<Cart>(c => c.UserId == userId).FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                await _carts.InsertOneAsync(cart);
            }
            return cart;
        }

        public async Task AddItemToCartByCartIdAsync(string cartId, CartItem item)
            {
                var update = Builders<Cart>.Update.Push(cart => cart.Items, item);
                await _carts.UpdateOneAsync(cart => cart.Id == cartId, update);
            }

            public async Task UpdateItemQuantityAsync(string cartId, string productId, int quantity)
            {
                var filter = Builders<Cart>.Filter.And(
                    Builders<Cart>.Filter.Eq(c => c.Id, cartId),
                    Builders<Cart>.Filter.ElemMatch(c => c.Items, i => i.ProductId == productId)
                );
                var update = Builders<Cart>.Update.Set("Items.$.Quantity", quantity);
                await _carts.UpdateOneAsync(filter, update);
            }

        public async Task AddItemToCartByUserIdAsync(string userId, CartItem item)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var update = Builders<Cart>.Update.Push(c => c.Items, item);
            await _carts.UpdateOneAsync(c => c.Id == cart.Id, update);
        }

        public async Task RemoveItemFromCartAsync(string cartId, string productId)
            {
                var update = Builders<Cart>.Update.PullFilter(c => c.Items, i => i.ProductId == productId);
                await _carts.UpdateOneAsync(c => c.Id == cartId, update);
            }

            public async Task ClearCartAsync(string cartId)
            {
                var update = Builders<Cart>.Update.Set(c => c.Items, new List<CartItem>());
                await _carts.UpdateOneAsync(c => c.Id == cartId, update);
            }
        
    }
}
