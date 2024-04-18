using FashionGrid.PaymentService.Models.Dtos;
using FashionGrid.PaymentService.Services.IServices;
using Stripe;
using Stripe.Checkout;

namespace FashionGrid.PaymentService.Services
{
    public class StripePaymentService : IStripePaymentService
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

    //    public async Task<string> CreateCheckoutSessionAsync(decimal cartTotal)
    //    {
    //        var options = new SessionCreateOptions
    //        {
    //            PaymentMethodTypes = new List<string>
    //    {
    //        "card",
    //    },
    //            LineItems = new List<SessionLineItemOptions>
    //    {
    //        new SessionLineItemOptions
    //        {
    //            PriceData = new SessionLineItemPriceDataOptions
    //            {
    //                UnitAmount = (long)(cartTotal * 100), // Convert total to cents
    //                Currency = "eur",
    //                ProductData = new SessionLineItemPriceDataProductDataOptions
    //                {
    //                    Name = "Total Cart Amount",
    //                    Images = new List<string> { "https://example.com/default-image.jpg" }, // Optional: default image or relevant image
    //                },
    //            },
    //            Quantity = 1, // Since it's a total, the quantity is 1
    //        }
    //    },
    //            Mode = "payment",
    //            SuccessUrl = "https://yourdomain.com/success?session_id={CHECKOUT_SESSION_ID}",
    //            CancelUrl = "https://yourdomain.com/cancel",
    //        };

    //        var service = new SessionService();
    //        Session session = await service.CreateAsync(options);
    //        return session.Url;
    //    }


    //}
}
