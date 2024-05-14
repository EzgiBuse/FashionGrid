using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using System.Threading.Tasks;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        private const string BaseCartUrl = "https://localhost:7240/Carts"; 

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = $"{BaseCartUrl}/{userId}"
            });
        }

        public async Task<ResponseDto?> AddToCartAsync(AddToCartRequest cartItem, string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartItem,
                Url = $"{BaseCartUrl}/{userId}"
            });
        }

        public async Task<ResponseDto?> RemoveItemFromCartAsync(string userId, string itemId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = $"{BaseCartUrl}/{userId}/{itemId}"
            });
        }

        public async Task<ResponseDto?> UpdateCartItemAsync(string userId, string itemId, int quantity)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = new { Quantity = quantity },
                Url = $"{BaseCartUrl}/{userId}/{itemId}"
            });
        }

        public async Task<ResponseDto?> ClearCartAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = $"{BaseCartUrl}/{userId}"
            });
        }
    }
}
