
using Cart.Application.DTO;

namespace Cart.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartObjectDTO> GetCartByCustomerId(int customerId);
        bool UpsertCartDetails(CartObjectDTO cartObject);
        bool RemoveCartContentDetails(int cartcontentdetailsid);
        Task<bool> ClearCart(int customerId);
    }
}
