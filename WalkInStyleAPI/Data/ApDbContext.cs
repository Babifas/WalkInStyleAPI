using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Models;

namespace WalkInStyleAPI.Data
{
    public class ApDbContext:DbContext
    {
      public ApDbContext(DbContextOptions<ApDbContext>options):base(options) 
        { 
        
        
        }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.user)
                .WithOne(u => u.cart)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.cart)
                .WithMany(c => c.carts)
                .HasForeignKey(c => c.CartId);
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.cartItems)
                .HasForeignKey(ci => ci.ProductId);



        }
    }
}
