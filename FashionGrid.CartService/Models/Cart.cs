using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FashionGrid.CartService.Models
{
    public class Cart
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        [BsonIgnore] // These properties are calculated and not stored directly
        public int TotalItemsCount => Items.Sum(item => item.Quantity);

        [BsonIgnore]
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    }
}
