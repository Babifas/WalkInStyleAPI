using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Category;

namespace WalkInStyleAPI.Services.Category_Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ApDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryService(ApDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<CategoryViewDto>> GetAllCategories()
        {
            var categories=await _dbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryViewDto>>(categories);
        }
        public async Task<CategoryViewDto> GetCategoryById(int id)
        {
            var category=await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CategoryViewDto>(category);
        }
        public async Task<bool> AddCategory(CategoryDto category)
        {
            var IsExist=await _dbContext.Categories.AnyAsync(c=>c.Name.ToLower() == category.Name.ToLower());
            if (!IsExist)
            {
                var _category=_mapper.Map<Category>(category);
                await _dbContext.Categories.AddAsync(_category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateCategory(CategoryDto category,int id)
        {
            var _category=await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            if (_category != null)
            {
                _mapper.Map(category,_category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var category=await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
