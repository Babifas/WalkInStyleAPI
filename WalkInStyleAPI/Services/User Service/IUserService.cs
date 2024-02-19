using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.User;

namespace WalkInStyleAPI.Services.User_Service
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterUserDto user);
        Task<List<UserViewDto>> GetAllUsers();
        Task<UserViewDto> GetUserById(int id);
        Task<User> Login(LoginDto user);
        Task BlockUser(int userid);
        Task UnblockUser(int userid);
    }
}
