using FashionGrid.CartService.Models;

namespace FashionGrid.CartService.Services.IServices
{
    public interface ICartService
    {
        
       
        Task ClearCartAsync(string userId);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddItemToCartAsync(string userId, CartItem item);
        Task DeleteCartItemAsync(string userId, string cartItemId);
        Task UpdateCartItemAsync(string userId, string cartItemId, int quantity);
       
    }
}
