using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.DTO
{
    public class OrderItemDTO
    {
        public int OrderItemDetailsId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public int DiscountPercentage { get; set; }
        public int OrderId { get; set; }        
    }
}
