using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.JWTVerification;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Cart;

namespace WalkInStyleAPI.Services.Cart_Service
{
    public class CartService:ICartService
    {
        private readonly ApDbContext _dbContext;
        private readonly IJWTService _jWTService;
        private readonly string HostUrl;
        public CartService(ApDbContext dbContext,IConfiguration configuration,IJWTService jWTService )
        {
            _dbContext = dbContext;
            _jWTService = jWTService;
            HostUrl = configuration["HostUrl:Url"];
        }
        public async Task<List<CartViewDto>> GetCart(string token)
        {
           
                int userid = _jWTService.GetUserIdFromToken(token);
                var cart = await _dbContext.Carts.Include(c => c.carts)
                    .ThenInclude(c => c.Product).FirstOrDefaultAsync(c => c.UserId == userid);
                if (cart == null)
                {
                    return new List<CartViewDto>();
                }
                var cartitems = cart.carts.Select(c => new CartViewDto
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.ProductName,
                    Image = HostUrl + c.Product.Image,
                    Price = c.Product.OfferPrice,
                    Quantity = c.Quantity,
                    TotalPrice = c.Product.OfferPrice * c.Quantity
                }).ToList();
                return cartitems;
        }
        public async Task<bool> AddToCart(string token, int productid)
        {
                int userid = _jWTService.GetUserIdFromToken(token);
                var user = await _dbContext.Users.Include(c => c.cart)
                    .ThenInclude(c => c.carts).FirstOrDefaultAsync(u => u.UserId == userid);
                var productExist = await _dbContext.Products.FirstOrDefaultAsync(u => u.ProductId == productid);
                if (user == null || productExist == null)
                {
                    throw new Exception("User Id or Product Id is not valid");
                }
                if (user.cart == null)
                {
                    user.cart = new Cart
                    {
                        UserId = userid
                    };
                    await _dbContext.Carts.AddAsync(user.cart);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var existingCartItem = user.cart.carts.FirstOrDefault(c => c.CartId == user.cart.CartId && c.ProductId == productid);
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += 1;
                        await _dbContext.SaveChangesAsync();
                        return false;
                    }
                }
                await _dbContext.CartItems.AddAsync(new CartItem
                {
                    CartId = user.cart.CartId,
                    ProductId = productid,
                    Quantity = 1
                });
                await _dbContext.SaveChangesAsync();
                return true;
            
        }
        public  async Task<bool> RemoveCart(string token,int productid)
        {
                int userid = _jWTService.GetUserIdFromToken(token);
                var user = await _dbContext.Users.Include(u => u.cart)
                    .ThenInclude(u => u.carts)
                    .FirstOrDefaultAsync(u => u.UserId == userid);
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
                if (user == null || product == null)
                {
                    throw new Exception("User Id or Product Id is not valid");
                }
                var cartitem = user.cart.carts.FirstOrDefault(ci => ci.ProductId == productid);
                if (cartitem == null)
                {
                    return false;
                }
                else
                {
                    _dbContext.CartItems.Remove(cartitem);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
          
            
        }
        public async Task<bool> IncrementQuantity(string token,int productid)
        {
            int userid = _jWTService.GetUserIdFromToken(token);
            var user = await _dbContext.Users.Include(u => u.cart)
              .ThenInclude(u => u.carts)
              .FirstOrDefaultAsync(u => u.UserId == userid);
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
            if (user == null || product == null)
            {
                throw new Exception("User Id or Product Id is not valid");
            }
            var cartitem = user.cart.carts.FirstOrDefault(ci => ci.ProductId == productid);
            if(cartitem == null)
            {
                return false;   
            }
            else
            {
                cartitem.Quantity += 1;
                await _dbContext.SaveChangesAsync();
                return true;
            }
          
        }
        public async Task<bool> DecrementQuantity(string token, int productid)
        {
            int userid = _jWTService.GetUserIdFromToken(token);
            var user = await _dbContext.Users.Include(u => u.cart)
                    .ThenInclude(u => u.carts)
                    .FirstOrDefaultAsync(u => u.UserId == userid);
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
            if (user == null || product == null)
            {
                throw new Exception("User Id or Product Id is not valid");
            }
            var cartitem = user.cart.carts.FirstOrDefault(ci => ci.ProductId == productid);
            if (cartitem == null)
            {
                return false;
            }
            else
            {
                if(cartitem.Quantity > 1)
                {
                    cartitem.Quantity -= 1;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
  
    }
}
