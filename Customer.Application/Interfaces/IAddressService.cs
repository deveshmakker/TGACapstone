using Customer.Application.DTO;

namespace Customer.Application.Interfaces
{
    public interface IAddressService
    {
        IEnumerable<AddressDTO> FindAddressByCustomerId(int customerid);
        AddressDTO FindAddressById(int addressid);
        AddressDTO AddAddress(AddressDTO address);
        AddressDTO EditAddress(int addressid, AddressDTO address);
        bool DeleteAddress(int addressid);
    }
}
