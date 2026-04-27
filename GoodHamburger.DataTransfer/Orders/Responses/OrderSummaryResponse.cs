namespace GoodHamburger.DataTransfer.Orders.Responses
{
    public class OrderSummaryResponse
    {
        public int TotalOrders { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}