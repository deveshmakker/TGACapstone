using AutoMapper;
using Cart.Application.DTO;
using Cart.Application.Interfaces;
using Cart.Common.Interfaces;
using Cart.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Services
{
    public class CartCacheService : ICartService
    {
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly TimeSpan _cacheTimeout = TimeSpan.FromDays(7);

        public CartCacheService(ICacheService cacheService,IMapper mapper,IProductService productService)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _productService = productService;
        }

        private string GetCartCacheKey(int customerId) => $"cart:{customerId}";

        public async Task<CartObjectDTO> GetCartByCustomerId(int customerId)
        {
            var cacheKey = GetCartCacheKey(customerId);
            var cart = await _cacheService.GetCacheValue<CartObjectDTO>(cacheKey);

            if (cart == null)
                return null;

            return _mapper.Map<CartObjectDTO>(cart);
        }

        public async Task<bool> ClearCart(int customerId)
        {
            var cacheKey = GetCartCacheKey(customerId);
            return await _cacheService.DeleteCacheValue(cacheKey);
        }

        public bool UpsertCartDetails(CartObjectDTO cartObjectDto)
        {
            if (cartObjectDto == null || cartObjectDto.cartDTO.CustomerId <= 0)
                return false;

            var cart = _mapper.Map<CartObjectDTO>(cartObjectDto);
            var cacheKey = GetCartCacheKey(cart.cartDTO.CustomerId);

            // Fire-and-forget cache update
            _ = _cacheService.SetCacheValue(cacheKey, cart, _cacheTimeout);
            return true;
        }
    }
}
