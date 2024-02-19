using WalkInStyleAPI.Models.DTOs.Wishlist;

namespace WalkInStyleAPI.Services.Whishlist_Service
{
    public interface IWishlistService
    {
        Task<bool> AddToWishlist(int userid, int productid);
        Task<bool> RemoveWishlist(int userid, int productid);
        Task<List<WishListViewDto>> GetWishlist(int userid);
    }
}
