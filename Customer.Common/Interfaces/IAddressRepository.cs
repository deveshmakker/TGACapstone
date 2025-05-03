using Customer.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Common.Interfaces
{
    public interface IAddressRepository
    {   
        IEnumerable<AddressDetails> FindAddressByCustomerId(int customerid);
        AddressDetails FindAddressById(int addressid);
        AddressDetails AddAddress(AddressDetails address);
        AddressDetails EditAddress(int addressid, AddressDetails address);        
        bool DeleteAddress(int addressid);
        
    }
}
