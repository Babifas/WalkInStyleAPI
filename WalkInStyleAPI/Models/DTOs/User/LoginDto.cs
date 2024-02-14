using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models.DTOs.User
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

    }
}
