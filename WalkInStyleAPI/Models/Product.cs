using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public decimal OrginalPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public Category category { get; set; }
        public List<CartItem> cartItems { get; set; }
        //public Wishlist wishlist { get; set; }
        //public List<Whishlist> whishlists { get; set; }


    }
}
