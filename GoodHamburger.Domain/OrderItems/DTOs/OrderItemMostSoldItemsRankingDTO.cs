namespace GoodHamburger.Domain.OrderItems.DTOs
{
    public class OrderItemMostSoldItemsRankingDTO
    {
        public virtual int Id { get; set; }
        public virtual string NameItem { get; set; } = string.Empty;
        public virtual int Quantity { get; set; }
    }
}