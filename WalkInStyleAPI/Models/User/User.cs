using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public int Password { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? Previlage { get; set; }


    }
}
