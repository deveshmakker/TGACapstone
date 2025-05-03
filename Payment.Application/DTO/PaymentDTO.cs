using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentSatus { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}
