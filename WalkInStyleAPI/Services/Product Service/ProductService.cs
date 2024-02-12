using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.Models.Product;

namespace WalkInStyleAPI.Services
{
    public class ProductService:IProductService
    {
        private readonly ApDbContext _context;
        public ProductService(ApDbContext context) 
        {
           _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var products=await _context.Products.ToListAsync();
            return products;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p=>p.ProductId==id);
            if (product != null) 
            {
                return product;
            }
            return null;
        }
        public async Task AddProduct(Product product)
        {
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
        }
        
    }
}
