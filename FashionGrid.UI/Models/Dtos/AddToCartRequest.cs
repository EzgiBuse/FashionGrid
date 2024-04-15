using MongoDB.Bson;

namespace FashionGrid.UI.Models.Dtos
{
    public class AddToCartRequest
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string productId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Attributes { get; set; }
    }

}
