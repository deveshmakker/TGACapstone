
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart.Common.Model
{
    public class CartDetails
    {
        [Key]
        public int CartId { get; set; }
        public int CustomerId { get; set; }        
        public ICollection<CartContentDetails> cartContents { get; set; }
        [NotMapped]
        public decimal CartTotal { get; set; }

    }
}
