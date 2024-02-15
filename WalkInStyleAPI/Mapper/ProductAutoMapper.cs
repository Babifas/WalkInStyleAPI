using AutoMapper;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Cart;
using WalkInStyleAPI.Models.DTOs.Category;
using WalkInStyleAPI.Models.DTOs.Product;
using WalkInStyleAPI.Models.DTOs.User;

namespace WalkInStyleAPI.Mapper
{
    public class ProductAutoMapper:Profile
    {
        public ProductAutoMapper() 
        {
           CreateMap<User,RegisterUserDto>().ReverseMap();
           CreateMap<User, UserViewDto>().ReverseMap();
           CreateMap<User,LoginDto>().ReverseMap();
           CreateMap<Category, CategoryViewDto>().ReverseMap();
           CreateMap<Category, CategoryDto>().ReverseMap();
           CreateMap<Product, ProductDto>().ReverseMap();
           CreateMap<Product,ProductViewDto>().ReverseMap();
        }
    }
}
