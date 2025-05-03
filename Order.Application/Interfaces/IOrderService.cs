
using Order.Application.DTO;

namespace Order.Application.Interfaces
{
    public interface IOrderService
    {
        public OrderObjectDTO GetOrder(int orderid);
        public Task<OrderDTO> CreateOrder(int customerid);        
        public bool CancelOrder(int orderid);
        public bool UpdatePaymentDetails(int orderid, int paymentid);
    }
}
