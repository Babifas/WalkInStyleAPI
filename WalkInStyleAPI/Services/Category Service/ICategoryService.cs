using WalkInStyleAPI.Models.DTOs.Category;

namespace WalkInStyleAPI.Services.Category_Service
{
    public interface ICategoryService
    {
        Task<List<CategoryViewDto>> GetAllCategories();
        Task<CategoryViewDto> GetCategoryById(int id);
        Task<bool> AddCategory(CategoryDto category);
        Task<bool> UpdateCategory(CategoryDto category, int id);
        Task<bool> DeleteCategory(int id);
    }
}
