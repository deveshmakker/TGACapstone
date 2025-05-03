using Customer.Common.Interfaces;
using Customer.Common.Model;
using Customer.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomerRepository(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public CustomerDetails AddCustomer(CustomerDetails customer)
        {
            _customerDbContext.Customers.Add(customer);
            _customerDbContext.SaveChanges();
            return customer;
        }

        public bool DeleteCustomer(int customerid)
        {
            var customer = FindCustomerById(customerid);
            _customerDbContext.Customers.Remove(customer);
            return (_customerDbContext.SaveChanges() != 0);
        }

        public CustomerDetails DisableCustomer(int customerid, CustomerDetails customer)
        {
            var customerDetails = FindCustomerById(customerid);
            customer.IsActive = false;
            _customerDbContext.Entry(customerDetails).CurrentValues.SetValues(customer);
            _customerDbContext.SaveChanges();
            return customer;
        }

        public CustomerDetails EditCustomer(int customerid, CustomerDetails customer)
        {
            var customerDetails = FindCustomerById(customerid);            
            _customerDbContext.Entry(customerDetails).CurrentValues.SetValues(customer);
            _customerDbContext.SaveChanges();
            return customer;
        }

        public CustomerDetails FindCustomerById(int customerid)
        {
            return _customerDbContext.Customers.Include(a=>a.Addresses).FirstOrDefault(c => c.CustomerId == customerid);
        }

        public IEnumerable<CustomerDetails> FindCustomerByKeyword(string keyword)
        {
            return _customerDbContext.Customers.Where(c => c.FirstName.Contains(keyword) || c.LastName.Contains(keyword) || c.Email.Contains(keyword)).ToList();
        }

        public IEnumerable<CustomerDetails> GetCustomers()
        {
            return _customerDbContext.Customers.Include(c => c.Addresses).ToList();
        }
    }
}
