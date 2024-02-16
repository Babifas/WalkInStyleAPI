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
        public DbSet<Whishlist> Whishlists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
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

            modelBuilder.Entity<Whishlist>()
                .HasMany(W => W.Products)
                .WithMany(P => P.whishlists)
                .UsingEntity(x => x.ToTable("WhilistPrdoduct"));

            modelBuilder.Entity<Whishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.whishlist)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o=>o.user)
                .WithMany(u => u.order)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.order)
                .WithMany(o=>o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        

        }
    }
}
