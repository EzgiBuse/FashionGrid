using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.UI.Models.Dtos
{
    public class OrderItemDto
    {
        
        public string ProductId { get; set; }

       
        public string ProductName { get; set; }

        
        public string DealerId { get; set; }

       
        public int Quantity { get; set; }

       
        public decimal Price { get; set; }
    }
}
