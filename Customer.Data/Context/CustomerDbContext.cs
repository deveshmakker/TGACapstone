using Customer.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Customer.Data.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<CustomerDetails> Customers {  get; set; }
        public DbSet<AddressDetails> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AddressDetails>()
                .HasOne(c => c.customer)
                .WithMany(a => a.Addresses)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<AddressDetails>()
                .HasData(
                new AddressDetails { AddressId = 1, CustomerId = 1, AddressLine1 = "House No 1", AddressLine2 = "Sector 1", AddressLine3 = string.Empty, City = "City A", Country = "Country A", Phone = "1234321", Pincode = "121212", State = "State 1"},
                new AddressDetails { AddressId = 2, CustomerId = 1, AddressLine1 = "House No 2", AddressLine2 = "Sector 2", AddressLine3 = string.Empty, City = "City B", Country = "Country A", Phone = "1234321", Pincode = "434343", State = "State 1" },
                new AddressDetails { AddressId = 3, CustomerId = 2, AddressLine1 = "House No 12", AddressLine2 = "Sector 12", AddressLine3 = string.Empty, City = "City C", Country = "Country A", Phone = "7846532", Pincode = "121212", State = "State 2" }
                );

            modelBuilder.Entity<CustomerDetails>()
                .HasData(
                new CustomerDetails { CustomerId = 1, DateOfBirth = Convert.ToDateTime("01-01-2001"), Email = "customer1@firstmail.com", FirstName = "Number", LastName = "One", IsActive = true, Password = "Password@1", PhoneNumber = "1234321" },
                new CustomerDetails { CustomerId = 2, DateOfBirth = Convert.ToDateTime("02-02-2002"), Email = "customer2@secondmail.com", FirstName = "Number", LastName = "Two", IsActive = true, Password = "Password@2", PhoneNumber = "7846532 " }

                );

        }
    }
}
