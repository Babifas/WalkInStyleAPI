﻿namespace WalkInStyleAPI.Models.DTOs.Cart
{
    public class CartViewDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
