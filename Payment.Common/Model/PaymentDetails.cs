using System.ComponentModel.DataAnnotations;


namespace Payment.Common.Model
{
    public class PaymentDetails
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public int PaymentAmount { get; set; }   
        public string PaymentSatus { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}
