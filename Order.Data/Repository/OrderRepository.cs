
using Order.Common.Model;
using Order.Common.Repository;
using Order.Data.Context;

namespace Order.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public OrderRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public bool CancelOrder(int orderid)
        {
            var orderToCancelFromDb = _orderDbContext.OrderDetails.Where(o => o.OrderId == orderid).FirstOrDefault();
            var orderToCancel = orderToCancelFromDb;
            orderToCancel.status = "Cancelled";
            _orderDbContext.Entry(orderToCancelFromDb).CurrentValues.SetValues(orderToCancel);
            return (_orderDbContext.SaveChanges() != 0);

        }

        public OrderDetails CreateOrder(OrderDetails orderDetails, IEnumerable<OrderItemDetails> orderItemDetails)
        {
            try
            {
                orderDetails.OrderId = _orderDbContext.OrderDetails.Max(x => x.OrderId) + 1;                

                foreach (var item in orderItemDetails)
                {
                    item.OrderId = orderDetails.OrderId;
                    item.order = null;
                }
                _orderDbContext.OrderDetails.Add(orderDetails);
                _orderDbContext.SaveChanges();
                _orderDbContext.OrderItemDetails.AddRange(orderItemDetails);
                _orderDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _orderDbContext.OrderDetails.FirstOrDefault(o => o.OrderId == orderDetails.OrderId);
        }

        public OrderDetails GetOrder(int orderid)
        {
            var orderItems = _orderDbContext.OrderItemDetails.Where(o => o.OrderId == orderid).ToList();
            var orderDetails = _orderDbContext.OrderDetails.Where(o => o.OrderId == orderid).FirstOrDefault();
            orderDetails.orderItems = orderItems;

            return orderDetails;
        }

        public bool UpdatePaymentDetails(int orderid, int paymentid)
        {
            var orderDetailsFromDb = _orderDbContext.OrderDetails.Where(o => o.OrderId == orderid).FirstOrDefault();
            orderDetailsFromDb.PaymentId = paymentid;
            orderDetailsFromDb.status = "Complete";
            _orderDbContext.Entry(orderDetailsFromDb).CurrentValues.SetValues(orderDetailsFromDb);
            return (_orderDbContext.SaveChanges() != 0);
        }
    }
}
