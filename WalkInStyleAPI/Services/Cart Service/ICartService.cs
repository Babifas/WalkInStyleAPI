using WalkInStyleAPI.Models.DTOs.Cart;

namespace WalkInStyleAPI.Services.Cart_Service
{
    public interface ICartService
    {
        Task<bool> AddToCart(int userid, int productid);
        Task<List<CartViewDto>> GetCart(int userid);
        Task RemoveCart(int userid, int productid);
    }
}
