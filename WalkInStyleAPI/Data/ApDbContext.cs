using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Models.Product;
using WalkInStyleAPI.Models.User;

namespace WalkInStyleAPI.Data
{
    public class ApDbContext:DbContext
    {
      public ApDbContext(DbContextOptions options):base(options) 
        { 
        
        
        }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
