using GoodHamburger.DataTransfer.OrderItems.Responses;
using GoodHamburger.DataTransfer.OrdersStatus.Responses;

namespace GoodHamburger.DataTransfer.Orders.Responses
{
    public class OrderResponse
    {
        public virtual int Id { get; set; }
        public virtual string CustomerName { get; set; } = null!;
        public virtual decimal Subtotal { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual int IdOrderStatus { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
        public virtual List<OrderItemResponse>? OrderItems { get; set; }
        public virtual string? OrderItemsDescription { get; set; }

        public virtual OrderStatusResponse? OrderStatus { get; set; }
    }
}