using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models.User
{
    public class UserViewDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
