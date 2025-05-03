
using AutoMapper;
using Cart.Application.DTO;
using Cart.Application.Interfaces;
using Cart.Common.Interfaces;
using Cart.Common.Model;

namespace Cart.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public CartService(ICartRepository cartRepository, IMapper mapper, IProductService productService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<bool> ClearCart(int customerId)
        {
            return _cartRepository.ClearCart(customerId);
        }

        public async Task<CartObjectDTO> GetCartByCustomerId(int customerId)
        {
            CartObjectDTO cartObject = null;
            var cartDetails = _cartRepository.GetCartByCustomerId(customerId);
            if (cartDetails == null)
            {
                cartObject = new CartObjectDTO();
            }
            else
            {
                cartObject = new()
                { cartDTO = _mapper.Map<CartDetailsDTO>(cartDetails) };
                cartObject.cartContentDTO = _mapper.Map<IEnumerable<CartContentDTO>>(_cartRepository.GetCartContentsByCartId(cartDetails.CartId));
                cartObject.cartDTO.CartTotal = 0;
                var products = await _productService.GetProducts();
                foreach (var item in cartObject.cartContentDTO)
                {
                    item.PricePerItem = products.FirstOrDefault(c => c.ProductId == item.ProductId).Price;
                    item.DiscountPercentage = products.FirstOrDefault(c => c.ProductId == item.ProductId).DiscountPercentage;

                    cartObject.cartDTO.CartTotal += (item.PricePerItem - ((item.PricePerItem * item.DiscountPercentage) / 100)) * item.Quantity;
                }
            }


            return cartObject;
        }

        public bool RemoveCartContentDetails(int cartcontentdetailsid)
        {
            return _cartRepository.RemoveCartContentDetails(cartcontentdetailsid);
        }

        public bool UpsertCartDetails(CartObjectDTO cartObject)
        {
            return _cartRepository.UpsertCartDetails(_mapper.Map<CartDetails>(cartObject.cartDTO), _mapper.Map<IEnumerable<CartContentDetails>>(cartObject.cartContentDTO));
        }
    }
}
