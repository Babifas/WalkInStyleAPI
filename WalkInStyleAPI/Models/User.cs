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


    }
}
