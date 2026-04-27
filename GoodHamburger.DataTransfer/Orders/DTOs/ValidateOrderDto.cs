using GoodHamburger.DataTransfer.MenuItems.Responses;
using GoodHamburger.DataTransfer.Orders.Responses;

namespace GoodHamburger.DataTransfer.Orders.DTOs
{
    public class ValidateOrderDto
    {
        public virtual OrderResponse Order { get; set; } = null!;
        public virtual IEnumerable<MenuItemResponse> MenuItemsOrder { get; set; } = null!;

        public ValidateOrderDto() { }
        public ValidateOrderDto(OrderResponse order, IEnumerable<MenuItemResponse> menuItemsOrder)
        {
            Order = order;
            MenuItemsOrder = menuItemsOrder;
        }
    }
}