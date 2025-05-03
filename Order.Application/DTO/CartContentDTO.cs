using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Application.DTO
{
    public class CartContentDTO
    {
        public int CartContentDetailsId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public int DiscountPercentage { get; set; }        
        public int CartId { get; set; }
    }
}
