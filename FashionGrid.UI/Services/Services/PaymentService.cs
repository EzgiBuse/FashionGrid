using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBaseService _baseService;
        private const string BaseCartUrl = "https://localhost:7204/api/Payments";  // Adjust the URL as needed

        public PaymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateStripeSessionAsync(List<CartItemDto> cartItems)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartItems,
                Url = $"{BaseCartUrl}/create-checkout-session"
            });
        }
    }
}
