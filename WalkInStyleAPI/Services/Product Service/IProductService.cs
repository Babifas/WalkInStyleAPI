
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Product;

namespace WalkInStyleAPI.Services
{
    public interface IProductService
    {

        Task<List<ProductViewDto>> GetAllProducts();
        Task<ProductViewDto> GetProductById(int id);
        Task<List<ProductViewDto>> GetProductsByCategory(string category);
        Task<bool> AddProduct(ProductDto product, IFormFile image);
        Task<bool> UpdateProduct(int id, [FromForm] ProductDto product, IFormFile image);
        Task<bool> DeleteProduct(int id);
        Task<List<ProductViewDto>> GetProductsPaginated(int PageNumber, int PageSize);
        Task<List<ProductViewDto>> SearchProduct(string product);
    }
}
