using Payment.Application.DTO;


namespace Payment.Application.Interfaces
{
    public interface IPaymentService
    {
        public PaymentDTO GetPaymentDetails(int paymentid);
        public bool ProcessPayment(PaymentDTO payment);
        public bool RefundPayment(int paymentid);
    }
}
