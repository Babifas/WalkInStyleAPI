using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class Whishlist
    {
        [Key]
        public int WhishlistId { get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set;}
    }
}
