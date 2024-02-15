using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> RemoveCart(int userid,int productid)
        {
            try
            {
                var res= await _cartService.RemoveCart(userid,productid);
                if (res)
                {
                    return Ok("Product remvoed from successfully");
                }
                else
                {
                    return BadRequest("The product doesn't exist in cart");
                }
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("IncrementQuantity")]
        [Authorize]
        public async Task<IActionResult> IncrementQuantity(int userid,int productid)
        {
            try
            {
                var res=await _cartService.IncrementQuantity(userid, productid);
                if (res)
                {
                    return Ok("Product quantity incremented");
                }
                else
                {
                    return BadRequest("The product doesn't exist in cart");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("DecrementQuantity")]
        [Authorize]
        public async Task<IActionResult> DecremntQuantity(int userid, int productid)
        {
            try
            {
                var res = await _cartService.DecrementQuantity(userid, productid);
                if (res)
                {
                    return Ok("Product quantity decremented");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
