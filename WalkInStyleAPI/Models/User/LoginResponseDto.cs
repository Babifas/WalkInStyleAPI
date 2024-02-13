namespace WalkInStyleAPI.Models.User
{
    public class LoginResponseDto
    {
        public User user { get; set; }
        public string? Token { get; set; }   
    }
}
