using FashionGrid.UI.Models.Dtos;

namespace FashionGrid.UI.Services.IServices
{
    public interface IPaymentService
    {
        Task<ResponseDto?> CreateStripeSessionAsync(decimal cartTotal);
    }
}
