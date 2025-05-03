using AutoMapper;
using Order.Application.DTO;
using Order.Application.Interfaces;
using Order.Common.Model;
using Order.Common.Repository;


namespace Order.Application.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderService(IProductService productService, ICartService cartService, IMapper mapper, IOrderRepository orderRepository)
        {
            _productService = productService;
            _cartService = cartService;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public bool CancelOrder(int orderid)
        {
            var order = GetOrder(orderid);
            foreach (var item in order.orderItems)
            {
                _productService.UpdateInventory(item.ProductId, item.Quantity * -1);
            }

            return _orderRepository.CancelOrder(orderid);
        }

        public async Task<OrderDTO> CreateOrder(int customerid)
        {
            var cart = await _cartService.GetCartForCustomer(customerid);

            var orderDto = _mapper.Map<OrderDetails>(_mapper.Map<OrderDTO>(cart.cartDTO));
            orderDto.status = "Created";
            orderDto.orderItems = null;
            var orderItemDto = _mapper.Map<IEnumerable<OrderItemDetails>>(_mapper.Map<IEnumerable<OrderItemDTO>>(cart.cartContentDTO));
            var orderDetails = _orderRepository.CreateOrder(orderDto, orderItemDto);

            var dto = _mapper.Map<OrderDTO>(orderDetails);
            var clearcart = await _cartService.ClearCart(customerid);
            return dto;
        }

        public OrderObjectDTO GetOrder(int orderid)
        {
            OrderObjectDTO orderObjectDTO = null;
            var order = _orderRepository.GetOrder(orderid);
            if (order == null)
            {
                orderObjectDTO = new OrderObjectDTO();
            }
            else
            {
                orderObjectDTO = new OrderObjectDTO
                {
                    order = _mapper.Map<OrderDTO>(order),
                    orderItems = _mapper.Map<IEnumerable<OrderItemDTO>>(order.orderItems)
                };
                
                orderObjectDTO.order.OrderTotal = 0;

                foreach (var item in orderObjectDTO.orderItems)
                {
                    orderObjectDTO.order.OrderTotal += (item.PricePerItem - ((item.PricePerItem * item.DiscountPercentage) / 100)) * item.Quantity;
                }                
            }
            return orderObjectDTO;
        }

        public bool UpdatePaymentDetails(int orderid, int paymentid)
        {
            var order = GetOrder(orderid);
            foreach (var item in order.orderItems)
            {
                _productService.UpdateInventory(item.ProductId, item.Quantity);
            }
            return _orderRepository.UpdatePaymentDetails(orderid, paymentid);
        }
    }
}
