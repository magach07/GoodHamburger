using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.Domain.Orders.Commands
{
    public class OrderInsertCommand
    {
        public virtual string CustomerName { get; protected set; } = null!;
        public virtual IEnumerable<MenuItem> MenuItemsOrder { get; set; } = null!;

        public OrderInsertCommand(string customerName, IEnumerable<MenuItem> menuItemsOrder)
        {
            CustomerName = customerName;
            MenuItemsOrder = menuItemsOrder;
        }
    }
}