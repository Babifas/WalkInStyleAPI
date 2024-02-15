using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Services.Cart_Service;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> UserCart(int userid)
        {
            try
            {
                return Ok(await _cartService.GetCart(userid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int userid,int productid)
        {
            try
            {
                var res=await _cartService.AddToCart(userid, productid);
                if (res)
                {
                    return Ok("Product added successfuly");
                }
                return Ok("Quantity incresed");
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occured : " +
                    $"{ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCart(int userid,int productid)
        {
            try
            {
                await _cartService.RemoveCart(userid,productid);
                return Ok("Product remvoed from successfully");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
