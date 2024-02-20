using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models.DTOs.Category;
using WalkInStyleAPI.Services.Category_Service;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _categoryService.GetAllCategories());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category != null)
                {
                    return Ok(category);
                }
                return NotFound("Category not found");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            try
            {
                var res = await _categoryService.AddCategory(category);
                if (res)
                {
                    return Ok("Successfuly  created a new category");
                }
                return BadRequest("The category already exist");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category,int id)
        {
            try
            {
                var res = await _categoryService.UpdateCategory(category,id);
                if (res)
                {
                    return Ok("Category updated successfully");   
                }
                return NotFound("The category not found");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var res = await _categoryService.DeleteCategory(id);
                if (res)
                {
                    return Ok("Category removed successfully");
                }
                return NotFound("The category not found");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
