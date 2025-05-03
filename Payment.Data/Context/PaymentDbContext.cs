
using Microsoft.EntityFrameworkCore;
using Payment.Common.Model;
using System.ComponentModel;

namespace Payment.Data.Context
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }

        public DbSet<PaymentDetails> paymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentDetails>().HasData(new PaymentDetails { CustomerId = 1, OrderId = 1, PaymentAmount = 14000, PaymentId = 1, PaymentSatus = "Complete", PaymentType = "Online" });
        }
    }
}
