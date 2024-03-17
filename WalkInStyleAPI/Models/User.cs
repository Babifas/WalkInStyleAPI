using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }
        public string? Role { get; set; }
        public bool isBlocked {  get; set; }
        public Cart cart { get; set; }
        public List<Wishlist> wishlists { get; set; }
        public List<Orders> order { get; set; }


    }
}
