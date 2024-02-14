
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Product;

namespace WalkInStyleAPI.Services
{
    public interface IProductService
    {

        Task<List<ProductViewDto>> GetAllProducts();
        Task<ProductViewDto> GetProductById(int id);
        Task<List<ProductViewDto>> GetProductsByCategory(string category);
        Task<bool> AddProduct(ProductDto product);
        Task<bool> UpdateProduct(ProductDto product, int id);
        Task<bool> DeleteProduct(int id);
    }
}
