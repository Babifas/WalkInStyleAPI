using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId {  get; set; }
        public User user {  get; set; }
        public List<CartItem> carts { get; set;}
    }
}
