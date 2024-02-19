using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Product;

namespace WalkInStyleAPI.Services
{
    public class ProductService:IProductService
    {
        private readonly ApDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApDbContext context,IMapper mapper) 
        {
           _context = context;
           _mapper = mapper;
        }
        public async Task<List<ProductViewDto>> GetAllProducts()
        {
            var products=await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductViewDto>>(products);
        }
        public async Task<ProductViewDto> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p=>p.ProductId==id);
            if (product != null) 
            {
                return _mapper.Map<ProductViewDto>(product);
            }
            return null;
        }
        public async Task<List<ProductViewDto>> GetProductsByCategory(string category)
        {
            var p=await _context.Products.Where(p=>p.category.Name.ToLower()==category.ToLower()).ToListAsync();

            return _mapper.Map<List<ProductViewDto>>(p);
        }
        public async Task<bool> AddProduct(ProductDto product)
        {
            var isExist = await _context.Products.AnyAsync(p => p.ProductName.ToLower() == product.ProductName.ToLower());
            if (isExist)
            {
                throw new Exception("This product already exist");
            }
            var categoryExist = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            if (categoryExist!=null)
            {
               var _product=_mapper.Map<Product>(product);
               _context.Products.Add(_product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateProduct(ProductDto product,int id)
        {
            var _product=await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            if(_product != null)
            {
                _mapper.Map(product, _product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product=await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}
