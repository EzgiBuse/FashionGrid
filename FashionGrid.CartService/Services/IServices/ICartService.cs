using FashionGrid.CartService.Models;

namespace FashionGrid.CartService.Services.IServices
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(string id);
        Task AddItemToCartByCartIdAsync(string cartId, CartItem item);
        Task AddItemToCartByUserIdAsync(string userId, CartItem item);
        Task UpdateItemQuantityAsync(string cartId, string productId, int quantity);
        Task RemoveItemFromCartAsync(string cartId, string productId);
        Task ClearCartAsync(string cartId);
    }
}
