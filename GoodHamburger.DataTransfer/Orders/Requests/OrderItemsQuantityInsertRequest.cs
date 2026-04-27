namespace GoodHamburger.DataTransfer.Orders.Requests
{
    public class OrderItemsQuantityInsertRequest
    {
        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
    }
}