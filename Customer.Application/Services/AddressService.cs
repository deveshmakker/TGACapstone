using AutoMapper;
using Customer.Application.DTO;
using Customer.Application.Interfaces;
using Customer.Common.Interfaces;
using Customer.Common.Model;

namespace Customer.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public AddressDTO AddAddress(AddressDTO address)
        {
            return _mapper.Map<AddressDTO>(_addressRepository.AddAddress(_mapper.Map<AddressDetails>(address)));
        }

        public bool DeleteAddress(int addressid)
        {
            return _addressRepository.DeleteAddress(addressid);
        }

        public AddressDTO EditAddress(int addressid, AddressDTO address)
        {
            return _mapper.Map<AddressDTO>(_addressRepository.EditAddress(addressid, _mapper.Map<AddressDetails>(address)));
        }

        public IEnumerable<AddressDTO> FindAddressByCustomerId(int customerid)
        {
            return _mapper.Map<IEnumerable<AddressDTO>>(_addressRepository.FindAddressByCustomerId(customerid));
        }

        public AddressDTO FindAddressById(int addressid)
        {
            return _mapper.Map<AddressDTO>(_addressRepository.FindAddressById(addressid));
        }
    }
}
