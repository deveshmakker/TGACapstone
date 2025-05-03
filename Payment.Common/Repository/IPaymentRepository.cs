using Payment.Common.Model;


namespace Payment.Common.Repository
{
    public interface IPaymentRepository
    {
        public PaymentDetails GetPaymentDetails(int paymentid);
        public bool ProcessPayment (PaymentDetails paymentDetails);
        public bool RefundPayment (int paymentid);
    }
}
