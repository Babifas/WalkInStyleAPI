using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string HostUrl;
        public ProductService(ApDbContext context,IMapper mapper,IWebHostEnvironment webHostEnvironment,IConfiguration configuration) 
        {
           _context = context;
           _mapper = mapper;
           _webHostEnvironment = webHostEnvironment;
           HostUrl= configuration["HostUrl:Url"];
        }
        public async Task<List<ProductViewDto>> GetAllProducts()
        {
            var products=await _context.Products.ToListAsync();
            products.Select(p => p.Image = HostUrl + p.Image).ToList();
            return _mapper.Map<List<ProductViewDto>>(products);
        }
        public async Task<ProductViewDto> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p=>p.ProductId==id);
            if (product != null) 
            {
                product.Image = HostUrl + product.Image;
                return _mapper.Map<ProductViewDto>(product);
            }
            return null;
        }

        public async Task<List<ProductViewDto>> GetProductsByCategory(string category)
        {
            var products=await _context.Products.Where(p=>p.category.Name.ToLower()==category.ToLower()).ToListAsync();
            products.Select(p=>p.Image = HostUrl + p.Image).ToList();
            return _mapper.Map<List<ProductViewDto>>(products);
        }

        public async Task<bool> AddProduct(ProductDto product,IFormFile image)
        {
            string productImage = null;

            if (image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Product", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                productImage = "/Uploads/Product/" + fileName;
            }
            else
            {
                productImage = "/Uploads/common/shoe.png";
            }

            var isExist = await _context.Products.AnyAsync(p => p.ProductName.ToLower() == product.ProductName.ToLower());
            if (isExist)
            {
                throw new Exception("This product already exist");
            }
            var categoryExist = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            if (categoryExist!=null)
            {
               var _product=_mapper.Map<Product>(product);
               _product.Image=productImage;
               _context.Products.Add(_product);
               await _context.SaveChangesAsync();
               return true;
            }
            return false;
        }

        public async Task<bool> UpdateProduct(int id, [FromForm] ProductDto product, IFormFile image)
        {
            var _product=await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            if(_product != null)
            {
                _product.ProductName=product.ProductName;
                _product.OrginalPrice=product.OrginalPrice;
                _product.OfferPrice=product.OfferPrice;
                _product.Description=product.Description;
                _product.CategoryId=product.CategoryId;
                _product.Brand=product.Brand;
                _product.Stock = product.Stock;
                if (image != null )
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    _product.Image = "/Uploads/Product/" + fileName;
                }
                else
                {
                    _product.Image=_product.Image;
                }
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
        public async Task<List<ProductViewDto>> GetProductsPaginated(int PageNumber,int PageSize)
        {
            var products=await _context.Products.Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();
            products.Select(p => p.Image = HostUrl + p.Image).ToList();
            return _mapper.Map<List<ProductViewDto>>(products);
        }
        public async Task<List<ProductViewDto>> SearchProduct(string product)
        {
            var products= await _context.Products.Where(p => p.ProductName.Contains(product)).ToListAsync();
            products.Select(p => p.Image = HostUrl + p.Image).ToList();
            return _mapper.Map<List<ProductViewDto>>(products);
        }
        
    }
}

