using FashionGrid.PaymentService.Models.Dtos;

namespace FashionGrid.PaymentService.Services.IServices
{
    public interface IStripePaymentService
    {
        public Task<string> CreateCheckoutSessionAsync(List<CartItemDto> CartItems);
       
    }
}
