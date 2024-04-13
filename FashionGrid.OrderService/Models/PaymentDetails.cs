using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.OrderService.Models
{
    public class PaymentDetails
    {
        [BsonElement("stripeSessionId")]
        public string StripeSessionId { get; set; } // For tracking Stripe Checkout session

        [BsonElement("paymentIntentId")]
        public string PaymentIntentId { get; set; } // For tracking the payment through Stripe

        [BsonElement("paymentStatus")]
        [BsonRepresentation(BsonType.String)]
        public PaymentStatus PaymentStatus { get; set; } // Example: Paid, Pending, Failed
    }
    public enum PaymentStatus
    {
        Pending,    // Payment has been initiated but not completed
        Paid,       // Payment is successfully completed
        Failed,     // Payment has failed
        Refunded    // Payment has been refunded
    }

}
