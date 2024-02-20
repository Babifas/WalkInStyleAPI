using WalkInStyleAPI.Models.DTOs.Cart;

namespace WalkInStyleAPI.Services.Cart_Service
{
    public interface ICartService
    {
        Task<bool> AddToCart(string token, int productid);
        Task<List<CartViewDto>> GetCart(string token);
        Task<bool> RemoveCart(string token, int productid);
        Task<bool> IncrementQuantity(string token, int productid);
        Task<bool> DecrementQuantity(string token, int productid);
    }
}
