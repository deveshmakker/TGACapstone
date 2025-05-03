using AutoMapper;
using Payment.Application.DTO;
using Payment.Common.Model;

namespace Payment.Application
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentDetails, PaymentDTO>().ReverseMap();
            });
            return config;
        }
    }
}
