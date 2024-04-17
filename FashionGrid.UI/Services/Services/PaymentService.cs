using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBaseService _baseService;
        private const string BasePaymentUrl = "https://localhost:7204/api/Payments";  // Adjust the URL as needed

        public PaymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateStripeSessionAsync(decimal cartTotal)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartTotal,
                Url = $"{BasePaymentUrl}/CreateCheckoutSession"
            });
        }
    }
}
