using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetWishlist()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                return Ok(await _wishlistService.GetWishlist(jwtToken));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToWishlist(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                var res=await _wishlistService.AddToWishlist(jwtToken, productid);
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
        [Authorize]
        public async Task<IActionResult> RemoveWishlist(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                var res = await _wishlistService.RemoveWishlist(jwtToken, productid);
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
