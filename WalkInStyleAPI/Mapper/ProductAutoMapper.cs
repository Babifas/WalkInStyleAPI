using AutoMapper;
using WalkInStyleAPI.Models.User;

namespace WalkInStyleAPI.Mapper
{
    public class ProductAutoMapper:Profile
    {
        public ProductAutoMapper() 
        {
           CreateMap<User,RegisterUserDto>().ReverseMap();
           CreateMap<User, UserViewDto>().ReverseMap();
           CreateMap<User,LoginDto>().ReverseMap();
        }
    }
}
