using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart.Common.Model
{
    public class CartContentDetails
    {
        [Key]
        public int CartContentDetailsId { get; set; }        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public int DiscountPercentage { get; set; }
        public int CartId { get; set; }
        public CartDetails Cart { get; set; }
    }
}
