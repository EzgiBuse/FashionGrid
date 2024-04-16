namespace FashionGrid.PaymentService.Models.Dtos
{
    public class CartDto
    {
        
        public string Id { get; set; }

       
        public string UserId { get; set; }  // User identifier

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

        // Total items count in the cart
        public int TotalItemsCount => Items.Sum(item => item.Quantity);

        // Total price for all items in the cart
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);

    }
}
