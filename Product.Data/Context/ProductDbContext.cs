using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore;
//using Product.Common.Model;
using Capstone.Product.Common.Model;
using Product.Common.Model;

namespace Product.Data.Context
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) 
        {

        }
        public DbSet<ProductDetails> Products { get; set; }
        public DbSet<CategoryDetails> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDetails>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<CategoryDetails>()
                .HasData(
                new CategoryDetails { Id = 1, Name = "Electronics", Description = "Electronic Items" },
                new CategoryDetails { Id = 2, Name = "Fashion", Description = "Fashion Products" }
                );

            modelBuilder.Entity<ProductDetails>()
                .HasData(
                new ProductDetails { ProductId = 1, Name = "Television", CategoryId = 1, Price = 20000, Description = "Television of Brand A", StockQuantity = 150, DiscountPercentage = 30, IsAvailable = true },
                new ProductDetails { ProductId = 2, Name = "Refrigerator", CategoryId = 1, Price = 50000, Description = "Refrigerator of Brand A", StockQuantity = 150, DiscountPercentage = 30, IsAvailable = true },
                new ProductDetails { ProductId = 3, Name = "Sneakers", CategoryId = 2, Price = 5000, Description = "Sneakers of Brand C", StockQuantity = 150, DiscountPercentage = 20, IsAvailable = true },
                new ProductDetails { ProductId = 4, Name = "Sports Shoes", CategoryId = 2, Price = 6000, Description = "Sports Shoes of Brand C", StockQuantity = 150, DiscountPercentage = 30, IsAvailable = true }
                );

        }
    }
}

