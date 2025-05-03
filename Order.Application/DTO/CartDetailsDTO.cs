namespace Order.Application.DTO
{
    public class CartDetailsDTO
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public decimal CartTotal { get; set; }
        //public ICollection<CartContentDTO> cartContents { get; set; }
    }
}
