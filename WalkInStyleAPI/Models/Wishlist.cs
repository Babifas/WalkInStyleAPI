using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
        public User User { get; set; }
        public Product Products { get; set;}
    }
}
