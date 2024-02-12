using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models.User
{
    public class RegisterUserDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }
    }
}
