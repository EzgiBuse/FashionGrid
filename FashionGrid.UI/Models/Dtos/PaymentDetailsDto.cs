using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.UI.Models.Dtos
{
    public class PaymentDetailsDto
    {
       
        public string StripeSessionId { get; set; } // For tracking Stripe Checkout session

        
        public string PaymentIntentId { get; set; } // For tracking the payment through Stripe

       
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
