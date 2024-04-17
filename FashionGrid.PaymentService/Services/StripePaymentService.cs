using FashionGrid.PaymentService.Models.Dtos;
using Stripe;
using Stripe.Checkout;

namespace FashionGrid.PaymentService.Services
{
    public class StripePaymentService
    {
        public StripePaymentService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }

        public async Task<string> CreateCheckoutSessionAsync(List<CartItemDto> items)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // Convert to cents
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Images = new List<string> { item.ImageUrl },
                        },
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = "https://yourdomain.com/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://yourdomain.com/cancel",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session.Url;
        }
    }


}
