using AutoMapper;
using Product.Application.DTO;
using Capstone.Product.Common.Model;
using Product.Common.Model;

namespace Product.Application
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetails, ProductDTO>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();

                cfg.CreateMap<CategoryDetails, CategoryDTO>().ReverseMap();
            });
            return config;
        }
    }
}
