
using Cart.Common.Interfaces;
using Cart.Common.Model;
using Cart.Data.Context;
using System.Linq;

namespace Cart.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _cartDbContext;
        public CartRepository(CartDbContext cartDbContext)
        {
            _cartDbContext = cartDbContext;
        }

        public bool ClearCart(int customerId)
        {            
            var cartDetailsFromDb = GetCartByCustomerId(customerId);
            var cartContentsFromDb = _cartDbContext.CartContentDetails.Where(c => c.CartId == cartDetailsFromDb.CartId).ToList();
            foreach (var item in cartContentsFromDb)
            {
                _cartDbContext.CartContentDetails.Remove(item);                
            }
            _cartDbContext.CartDetails.Remove(cartDetailsFromDb);
            return (_cartDbContext.SaveChanges() != 0);
        }

        public CartDetails GetCartByCustomerId(int customerid)
        {
            return _cartDbContext.CartDetails.Where(c => c.CustomerId == customerid).FirstOrDefault();
        }

        public IEnumerable<CartContentDetails> GetCartContentsByCartId(int cartId)
        {
            return _cartDbContext.CartContentDetails.Where(c => c.CartId == cartId).ToList();
        }

        public bool RemoveCartContentDetails(int cartcontentdetailsid)
        {
            var cartContentToRemove = _cartDbContext.CartContentDetails.Where(c => c.CartContentDetailsId == cartcontentdetailsid).FirstOrDefault();
            var totalCartContents = _cartDbContext.CartContentDetails.Where(c => c.CartId == cartContentToRemove.CartId).Count();
            _cartDbContext.CartContentDetails.Remove(cartContentToRemove);
            if(totalCartContents == 1)
            {
                var cartDetailsToRemove = _cartDbContext.CartDetails.Where(c => c.CartId == cartContentToRemove.CartId).FirstOrDefault();
                _cartDbContext.CartDetails.Remove(cartDetailsToRemove);
            }

            return (_cartDbContext.SaveChanges() != 0);
        }

        public bool UpsertCartDetails(CartDetails cartDetails, IEnumerable<CartContentDetails> cartContentDetails)
        {
            try
            {
                var cartDetailfromDb = _cartDbContext.CartDetails.Where(c => c.CustomerId == cartDetails.CustomerId).FirstOrDefault();
                if (cartDetailfromDb == null)
                {
                    cartDetails.cartContents = null;
                    _cartDbContext.CartDetails.Add(cartDetails);
                    foreach (var item in cartContentDetails)
                    {
                        item.Cart = null;
                    }
                        _cartDbContext.CartContentDetails.AddRange(cartContentDetails);
                }
                else
                {
                    foreach (var item in cartContentDetails)
                    {
                        item.Cart = null;
                        var cartContentfromDb = _cartDbContext.CartContentDetails.Where(c => c.CartId == item.CartId && c.ProductId == item.ProductId).FirstOrDefault();
                        if (cartContentfromDb == null)
                        {
                            //_cartDbContext.Entry(cartContentDetails).CurrentValues.SetValues(item);
                            _cartDbContext.CartContentDetails.Add(item);
                        }
                        else
                        {
                            cartContentfromDb.Quantity += 1;
                            _cartDbContext.Entry(cartContentfromDb).CurrentValues.SetValues(cartContentfromDb);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (_cartDbContext.SaveChanges() != 0);
        }
    }
}
