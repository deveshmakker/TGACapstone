using AutoMapper;
using Cart.Application.DTO;
using Cart.Common.Model;
namespace Cart.Application
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartContentDetails, CartContentDTO>()
                .ForMember(dest => dest.CartContentDetailsId, opt => opt.MapFrom(src => src.CartContentDetailsId))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart.CartId))
                .ReverseMap();

                cfg.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
            });
            return config;
        }
    }
}
