using GoodHamburger.DataTransfer.MenuItems.Responses;
using GoodHamburger.DataTransfer.Orders.Responses;

namespace GoodHamburger.DataTransfer.OrderItems.Responses
{
    public class OrderItemResponse
    {
        public virtual int Id { get; set; }
        public virtual int IdOrder { get; set; }
        public virtual int IdMenuItem { get; set; }

        public virtual MenuItemResponse MenuItem { get; set; } = null!;
    }
}