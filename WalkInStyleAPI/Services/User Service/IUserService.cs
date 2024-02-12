using WalkInStyleAPI.Models.User;

namespace WalkInStyleAPI.Services.User_Service
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterUserDto user);
        Task<List<UserViewDto>> GetAllUsers();
        Task<UserViewDto> GetUserById(int id);
        Task<bool> Login(LoginDto user);
    }
}
