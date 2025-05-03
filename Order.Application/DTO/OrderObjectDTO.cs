using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.DTO
{
    public class OrderObjectDTO
    {
        public OrderDTO order {  get; set; }
        public IEnumerable<OrderItemDTO> orderItems { get; set; }
    }
}
