using Customer.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Common.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDetails> GetCustomers();
        CustomerDetails FindCustomerById(int customerid);
        IEnumerable<CustomerDetails> FindCustomerByKeyword(string keyword);
        CustomerDetails AddCustomer(CustomerDetails customer);
        CustomerDetails EditCustomer(int customerid, CustomerDetails customer);
        bool DeleteCustomer(int customerid);
        CustomerDetails DisableCustomer(int customerid, CustomerDetails customer);

    }
}
