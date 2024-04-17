namespace FashionGrid.PaymentService.Services.IServices
{
    public interface IStripePaymentService
    {
        public Task<string> CreateCheckoutSessionAsync(decimal cartTotal);
    }
}
