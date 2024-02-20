﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddNewOrder(int userid)
        {
            try
            {
                var res=await _orderService.AddNewOrder(userid);
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
        public async Task<IActionResult> OrderDetails(int userid)
        {
            try
            {
                return Ok(await _orderService.OrderDetails(userid));

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("TotalRevanue")]
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
    }
}
