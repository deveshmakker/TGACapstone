
using AutoMapper;
using Customer.Application.DTO;
using Customer.Application.Interfaces;
using Customer.Common.Interfaces;
using Customer.Common.Model;

namespace Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public CustomerDTO AddCustomer(CustomerDTO customer)
        {
            return _mapper.Map<CustomerDTO>(_customerRepository.AddCustomer(_mapper.Map<CustomerDetails>(customer)));
        }

        public bool DeleteCustomer(int customerid)
        {
            return _customerRepository.DeleteCustomer(customerid);
        }

        public CustomerDTO DisableCustomer(int customerid, CustomerDTO customer)
        {
            return _mapper.Map<CustomerDTO>(_customerRepository.DisableCustomer(customerid, _mapper.Map<CustomerDetails>(customer)));
        }

        public CustomerDTO EditCustomer(int customerid, CustomerDTO customer)
        {
            return _mapper.Map<CustomerDTO>(_customerRepository.EditCustomer(customerid, _mapper.Map<CustomerDetails>(customer)));
        }

        public CustomerDTO FindCustomerById(int customerid)
        {
            return _mapper.Map<CustomerDTO>(_customerRepository.FindCustomerById(customerid));
        }

        public IEnumerable<CustomerDTO> FindCustomerByKeyword(string keyword)
        {
            return _mapper.Map<IEnumerable<CustomerDTO>>(_customerRepository.FindCustomerByKeyword(keyword));
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _mapper.Map<IEnumerable<CustomerDTO>>(_customerRepository.GetCustomers());
        }
    }
}
