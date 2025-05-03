using Order.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Repository
{
    public interface IOrderRepository
    {
        public OrderDetails GetOrder(int orderid);
        public OrderDetails CreateOrder(OrderDetails orderDetails, IEnumerable<OrderItemDetails> orderItemDetails);
        public bool CancelOrder(int orderid);
        public bool UpdatePaymentDetails(int orderid, int paymentid);
    }
}
