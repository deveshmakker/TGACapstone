
using Microsoft.EntityFrameworkCore;
using Order.Common.Model;

namespace Order.Data.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) 
        {

        }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderItemDetails> OrderItemDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItemDetails>()
                .HasOne(c => c.order)
                .WithMany(a => a.orderItems)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails { OrderId = 1, OrderTotal = 14000, CustomerId = 1, AddressId = 1, PaymentId = 1, status = "Complete", StripeSessionId = "1" },
                 new OrderDetails { OrderId = 2, OrderTotal = 8200, CustomerId = 2, AddressId = 3, PaymentId = 2, status = "Complete", StripeSessionId = "2" });

            modelBuilder.Entity<OrderItemDetails>().HasData(
                new OrderItemDetails { OrderId = 1, OrderItemDetailsId = 1, PricePerItem = 20000, DiscountPercentage = 30, ProductId = 1, Quantity = 1 },
                new OrderItemDetails { OrderId = 2, OrderItemDetailsId = 2, PricePerItem = 5000, DiscountPercentage = 20, ProductId = 3, Quantity = 1 },
                new OrderItemDetails { OrderId = 2, OrderItemDetailsId = 3, PricePerItem = 6000, DiscountPercentage = 30, ProductId = 4, Quantity = 1 });

        }

    }
}
