using Cart.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.DTO
{
    public class CartDetailsDTO
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public decimal CartTotal { get; set; }
        //public ICollection<CartContentDTO> cartContents { get; set; }
    }
}
