using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FashionGrid.CartService.Models
{
    public class CartItem
    {
        [BsonId] // Defines this property as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Ensures the ID is treated as an ObjectId
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("attributes")]
        public List<string> Attributes { get; set; } = new List<string>();

        [BsonIgnore] // This property is ignored by MongoDB and not stored in the database
        public decimal TotalPrice => Price * Quantity;
    }
}
