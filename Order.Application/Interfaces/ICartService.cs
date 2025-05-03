
using Order.Application.DTO;

namespace Order.Application.Interfaces
{
    public interface ICartService
    {
        public Task<CartObjectDTO> GetCartForCustomer(int  customerId);
        public Task<bool> ClearCart(int customerid);
    }
}
