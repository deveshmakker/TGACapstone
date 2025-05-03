
using Cart.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cart.Data.Context
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {

        }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartContentDetails> CartContentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartContentDetails>()
                .HasOne(c => c.Cart)
                .WithMany(cc => cc.cartContents)
                .HasForeignKey(f => f.CartId);


            modelBuilder.Entity<CartDetails>().HasData(
                new CartDetails { CartId = 1, CartTotal = 14000, CustomerId = 1 },
                new CartDetails { CartId = 2, CartTotal = 8200, CustomerId = 2 });

            modelBuilder.Entity<CartContentDetails>().HasData(
                new CartContentDetails { CartId = 1, CartContentDetailsId = 1, PricePerItem = 20000, DiscountPercentage = 30, ProductId = 1, Quantity = 1 },
                new CartContentDetails { CartId = 2, CartContentDetailsId = 2, PricePerItem = 5000, DiscountPercentage = 20, ProductId = 3, Quantity = 1 },
                new CartContentDetails { CartId = 2, CartContentDetailsId = 3, PricePerItem = 6000, DiscountPercentage = 30, ProductId = 4, Quantity = 1 });
        }
    }
}
