using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models.Product;
using WalkInStyleAPI.Services;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        { 
           _productService = productService;
        }
        [HttpGet("Get All Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _productService.GetAllProducts());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product != null)
                {
                    return Ok(product);
                }
                return NotFound("User Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task AddProduct([FromBody] Product product)
        {
            try
            {
                await _productService.AddProduct(product);
            }catch (Exception ex)
            {
                 BadRequest(ex.Message);
            }
        }
    }
}
