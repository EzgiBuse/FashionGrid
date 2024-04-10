using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FashionGrid.ProductCatalogService.Models.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public string UserId { get; set; } // Reference to the Seller

        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
