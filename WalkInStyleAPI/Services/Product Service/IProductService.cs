
using WalkInStyleAPI.Models.Product;

namespace WalkInStyleAPI.Services
{
    public interface IProductService
    {

        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
    }
}
