using GoodHamburger.DataTransfer.MenuItems.Responses;

namespace GoodHamburger.DataTransfer.Orders.Responses
{
    public class OrderAndItemsInsertResponse
    {
        public virtual OrderResponse Order { get; set; } = null!;
        public virtual IEnumerable<MenuItemResponse> MenuItemsOrder { get; set; } = null!;

        public OrderAndItemsInsertResponse(OrderResponse order, IEnumerable<MenuItemResponse> menuItemsOrder)
        {
            Order = order;
            MenuItemsOrder = menuItemsOrder;
        }
    }
}
