namespace FashionGrid.UI.Models.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
        public string UserId { get; set; } 
        public Dictionary<string, List<string>> Attributes { get; set; }

        public List<string> ImageUrls { get; set; } 
    }
}
