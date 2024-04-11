using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FashionGrid.ProductCatalogService.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public string UserId { get; set; } // Reference to the Seller

        // Dynamic Attributes
        [BsonRepresentation(BsonType.Document)]
        public Dictionary<string, List<string>> Attributes { get; set; } = new Dictionary<string, List<string>>();

        public List<string> ImageUrls { get; set; } = new List<string>(); 
    }
}
