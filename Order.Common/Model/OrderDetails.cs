
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Common.Model
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public int PaymentId { get; set; }
        public ICollection<OrderItemDetails> orderItems { get; set; }
        [NotMapped]
        public decimal OrderTotal { get; set; }
        public string status { get; set; }
        public string? StripeSessionId { get; set; }
    }
}
