
using Cart.Application.DTO;

namespace Cart.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartObjectDTO> GetCartByCustomerId(int customerId);
        bool UpsertCartDetails(CartObjectDTO cartObject);
        Task<bool> ClearCart(int customerId);
    }
}
