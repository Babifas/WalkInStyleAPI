namespace WalkInStyleAPI.Models.DTOs.Wishlist
{
    public class WishListViewDto
    {
        public int wishlistId { get; set; }
        public string productName { get; set; }
        public string Description { get; set; }
        public string ProductImage {  get; set; }
        public decimal Price {  get; set; }
    }
}
