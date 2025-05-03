using Customer.Common.Interfaces;
using Customer.Common.Model;
using Customer.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        public AddressRepository(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public AddressDetails AddAddress(AddressDetails address)
        {            
            _customerDbContext.Addresses.Add(address);
            _customerDbContext.SaveChanges();
            return address;
        }

        public bool DeleteAddress(int addressid)
        {
            var address = FindAddressById(addressid);
            _customerDbContext.Addresses.Remove(address);
            return (_customerDbContext.SaveChanges() != 0);

        }

        public AddressDetails EditAddress(int addressid, AddressDetails address)
        {
            var addressDetails = FindAddressById(addressid);
            _customerDbContext.Entry(addressDetails).CurrentValues.SetValues(address);
            _customerDbContext.SaveChanges();
            return address;
        }

        public IEnumerable<AddressDetails> FindAddressByCustomerId(int customerid)
        {
            return _customerDbContext.Addresses.Where(a => a.CustomerId == customerid).ToList();
        }

        public AddressDetails FindAddressById(int addressid)
        {
            return _customerDbContext.Addresses.FirstOrDefault(a => a.AddressId == addressid);
        }
    }
}
