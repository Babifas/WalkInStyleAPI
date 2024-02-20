using WalkInStyleAPI.Models.DTOs.Wishlist;

namespace WalkInStyleAPI.Services.Whishlist_Service
{
    public interface IWishlistService
    {
        Task<bool> AddToWishlist(string token, int productid);
        Task<bool> RemoveWishlist(string token, int productid);
        Task<List<WishListViewDto>> GetWishlist(string token);
    }
}
