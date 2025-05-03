using AutoMapper;
using Customer.Application.DTO;
using Customer.Common.Model;

namespace Customer.Application
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressDetails, AddressDTO>().ReverseMap();
                    

                cfg.CreateMap<CustomerDetails, CustomerDTO>().ReverseMap();
            });
            return config;
        }
    }
}
