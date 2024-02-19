using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Product;
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
                return NotFound("Product Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ProductsByCategory/{category}")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            try
            {
                return Ok(await _productService.GetProductsByCategory(category));
            }catch (Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            try
            {
                var res=await _productService.AddProduct(product);
                if (res)
                {
                    return Ok("Product added successfully");
                }
                return BadRequest("Category does not exist");
            }catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product,int id)
        {
            try
            {
                var res = await _productService.UpdateProduct(product, id);
                if (res)
                {
                    return Ok("Product updated successfuly");
                }
                return NotFound("The product not found");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] int id)
        {
            try
            {
                var res = await _productService.DeleteProduct(id);
                if (res)
                {
                    return Ok("Product deleted successfully");
                }
                return NotFound("The product not found");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
