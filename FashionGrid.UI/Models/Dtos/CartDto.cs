namespace FashionGrid.UI.Models.Dtos
{
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

        // Total items count in the cart
        public int TotalItemsCount => Items.Sum(item => item.Quantity);

        // Total price for all items in the cart
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);

    }
}
