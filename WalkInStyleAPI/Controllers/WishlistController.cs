using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Services.Whishlist_Service;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        [HttpGet]
        public async Task<IActionResult> GetWishlist(int userid)
        {
            try
            {
                return Ok(await _wishlistService.GetWishlist(userid));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int userid,int productid)
        {
            try
            {
               var res=await _wishlistService.AddToWishlist(userid, productid);
                if (res)
                {
                    return Ok("Product added to wishlist");
                }
                   return BadRequest("Product alreay in wishlist");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveWishlist(int userid,int productid)
        {
            try
            {
                var res = await _wishlistService.RemoveWishlist(userid, productid);
                if (res)
                {
                    return Ok("Product removed from wishlist");
                }
                return NotFound("Product doesn't exist in wishlist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
