namespace GoodHamburger.DataTransfer.OrderItems.Responses
{
    public class OrderItemMostSoldItemsRankingResponse
    {
        public virtual int Id { get; set; }
        public virtual string NameItem { get; set; } = string.Empty;
        public virtual int Quantity { get; set; }
    }
}