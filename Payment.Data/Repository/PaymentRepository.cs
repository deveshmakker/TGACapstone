using Payment.Common.Model;
using Payment.Common.Repository;
using Payment.Data.Context;


namespace Payment.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _paymentDbContext;
        public PaymentRepository(PaymentDbContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
        }

        public PaymentDetails GetPaymentDetails(int paymentid)
        {
            return _paymentDbContext.paymentDetails.Where(p => p.PaymentId == paymentid).FirstOrDefault();
        }

        public bool ProcessPayment(PaymentDetails paymentDetails)
        {
            _paymentDbContext.paymentDetails.Add(paymentDetails);
            return (_paymentDbContext.SaveChanges() != 0);
        }

        public bool RefundPayment(int paymentid)
        {
            var payment = GetPaymentDetails(paymentid);
            payment.PaymentSatus = "Refunded";
            _paymentDbContext.Entry(payment).CurrentValues.SetValues(payment);
            return (_paymentDbContext.SaveChanges() != 0);
        }
    }
}
