using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.UI.Models.Dtos
{
    public class OrderDto
    {
        
        public string Id { get; set; }

        
        public string UserId { get; set; }

       
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        
        public decimal TotalAmount { get; set; }

       
        public DateTime OrderDate { get; set; }

       
        public OrderStatus Status { get; set; } 

        
        public PaymentDetailsDto PaymentDetails { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled
    }
}

