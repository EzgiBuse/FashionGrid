﻿namespace FashionGrid.UI.Models.Dtos
{
    public class CartItemDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Attributes { get; set; }
        // Calculate the total price of this item based on quantity
        public decimal TotalPrice => Price * Quantity;
    }
}
