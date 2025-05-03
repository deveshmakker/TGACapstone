
namespace Cart.Application.DTO
{
    public class CartObjectDTO
    {
        public CartDetailsDTO cartDTO { get; set; }
        public IEnumerable<CartContentDTO> cartContentDTO { get; set; }
    }
}
