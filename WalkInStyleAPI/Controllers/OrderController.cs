using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models.DTOs.Order;
using WalkInStyleAPI.Services.Order_Sevice;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase  
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        { 
             _orderService = orderService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewOrder()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                var res=await _orderService.AddNewOrder(jwtToken);
                if (res)
                {
                    return Ok("Ordered successfully");
                }
                else
                {
                    return NotFound("No product in cart");
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OrderDetails()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                return Ok(await _orderService.OrderDetails(jwtToken));

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("TotalRevanue")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> TotalRevanue()
        {
            try
            {
                var res = await _orderService.TotalRevanue();
                return Ok($"Total revanue is {res}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("TotalProductsPurchased")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TotalProductsPurchased()
        {
            try
            {
                var res = await _orderService.TotalProductsPurchased();
                return Ok($"Total {res} products purchased  ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("payment")]
        [Authorize]
        public ActionResult Payment(RazorpayDto razorpay)
        {
            try
            {
                if (razorpay == null)
                {
                    return BadRequest("razorpay details must not null here");
                }
                var res = _orderService.Payment(razorpay);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        [HttpPost("order-create")]
        [Authorize]
        public async Task<ActionResult> createOrder(long price)
        {
            try
            {
                if (price <= 0)
                {
                    return BadRequest("enter a valid money ");
                }
                var orderId = await _orderService.OrderCreate(price);
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
    }
}
