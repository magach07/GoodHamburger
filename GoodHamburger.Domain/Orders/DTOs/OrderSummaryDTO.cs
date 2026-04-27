namespace GoodHamburger.Domain.Orders.DTOs
{
    public class OrderSummaryDTO
    {
        public virtual int TotalOrders { get; set; }
        public virtual decimal SubtotalAmount { get; set; }
        public virtual decimal TotalDiscount { get; set; }
        public virtual decimal TotalAmount { get; set; }

        public OrderSummaryDTO()
        { }
    }
}