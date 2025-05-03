
using AutoMapper;
using Order.Application.DTO;
using Order.Common.Model;

namespace Order.Application
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderItemDetails, OrderItemDTO>()
                .ForMember(dest => dest.OrderItemDetailsId, opt => opt.MapFrom(src => src.OrderItemDetailsId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.order.OrderId))
                .ReverseMap();

                cfg.CreateMap<OrderDetails, OrderDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();


                cfg.CreateMap<CartDetailsDTO, OrderDTO>()
                .ForMember(dest => dest.OrderTotal, u => u.MapFrom(src => src.CartTotal)).ReverseMap();

                cfg.CreateMap<CartContentDTO, OrderItemDTO>()
                .ForMember(dest => dest.ProductId, u => u.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.PricePerItem, u => u.MapFrom(src => src.PricePerItem));

                cfg.CreateMap<OrderItemDTO, CartDetailsDTO>(); 

            });
            return config;
        }
    }
}
