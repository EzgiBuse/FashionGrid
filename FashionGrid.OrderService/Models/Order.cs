using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.OrderService.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("orderItems")]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public OrderStatus Status { get; set; } 

        [BsonElement("paymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled
    }
}

