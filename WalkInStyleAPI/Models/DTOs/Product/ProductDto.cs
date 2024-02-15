namespace WalkInStyleAPI.Models.DTOs.Product
{
    public class ProductDto
    {
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public decimal OrginalPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}
