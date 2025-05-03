
using Cart.Common.Model;

namespace Cart.Common.Interfaces
{
    public interface ICartRepository
    {
        CartDetails GetCartByCustomerId(int customerd);
        IEnumerable<CartContentDetails> GetCartContentsByCartId(int cartId);
        bool UpsertCartDetails(CartDetails cartDetails, IEnumerable<CartContentDetails> cartContentDetails);
        bool RemoveCartContentDetails(int cartcontentdetailsid);
        bool ClearCart(int customerId);
    }
}
