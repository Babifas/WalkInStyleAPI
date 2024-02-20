using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Wishlist;

namespace WalkInStyleAPI.Services.Whishlist_Service
{
    public class WishlistService:IWishlistService
    {
        private readonly ApDbContext _dbContext;
        private readonly string HostUrl;
        public WishlistService(ApDbContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            HostUrl = configuration["HostUrl:Url"];
        }
        public async Task<bool> AddToWishlist(int userid,int productid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            var product=await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
            if (user == null||product==null)
            {
                throw new Exception("User id or product id not valid");
            }
            var isExist=await _dbContext.Wishlists.FirstOrDefaultAsync(u => u.UserId == userid&& u.ProductId==productid);
            if (isExist==null)
            {
                Wishlist wishlist = new Wishlist
                {
                    UserId = userid,
                    ProductId = productid,

                };
                await _dbContext.Wishlists.AddAsync(wishlist);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false; 
        }
        public async Task<bool> RemoveWishlist(int userid,int productid)
        {
            var user = await _dbContext.Users.Include(u=>u.wishlists).FirstOrDefaultAsync(u => u.UserId == userid);
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
            if (user == null || product == null)
            {
                throw new Exception("User id or product id not valid");
            }
            var wishlist=user.wishlists.FirstOrDefault(w=>w.UserId==userid&&w.ProductId==productid);
            if (wishlist == null)
            {
                return false;
            }
            _dbContext.Wishlists.Remove(wishlist);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<WishListViewDto>> GetWishlist(int userid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            if (user == null)
            {
                throw new Exception("User id is not valid");
            }
            var wishlists = await _dbContext.Wishlists
               .Include(u => u.Products)
               .Where(u => u.UserId == userid).ToListAsync();
            if (wishlists == null)
            {
                return new List<WishListViewDto>();
            }
            var wishlist = wishlists.Select(w => new WishListViewDto
            {
                wishlistId = w.WishlistId,
                productName = w.Products.ProductName,
                Description=w.Products.Description,
                ProductImage=HostUrl + w.Products.Image,
                Price=w.Products.OfferPrice
            }).ToList();
            return wishlist;
        }
    }
}
