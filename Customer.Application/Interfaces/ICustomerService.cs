

using Customer.Application.DTO;

namespace Customer.Application.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetCustomers();
        CustomerDTO FindCustomerById(int customerid);
        IEnumerable<CustomerDTO> FindCustomerByKeyword(string keyword);
        CustomerDTO AddCustomer(CustomerDTO customer);
        CustomerDTO EditCustomer(int customerid, CustomerDTO customer);
        bool DeleteCustomer(int customerid);
        CustomerDTO DisableCustomer(int customerid, CustomerDTO customer);
    }
}
