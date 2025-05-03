using System.ComponentModel.DataAnnotations;

namespace Order.Common.Model
{
    public class OrderItemDetails
    {
        [Key]
        public int OrderItemDetailsId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public int DiscountPercentage { get; set; }
        public int OrderId { get; set; }
        public OrderDetails order { get; set; }
    }
}
