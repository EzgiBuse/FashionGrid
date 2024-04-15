using FashionGrid.UI.Models.Dtos;

namespace FashionGrid.UI.Services.IServices
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> AddToCartAsync(AddToCartRequest cartItem, string userId);
        Task<ResponseDto?> RemoveItemFromCartAsync(string userId, string itemId);
        Task<ResponseDto?> UpdateCartItemAsync(string userId, string itemId, int quantity);
        Task<ResponseDto?> ClearCartAsync(string userId);
    }
}
