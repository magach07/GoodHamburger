using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.Domain.Orders.Commands
{
    public class ValidatedOrderCommand
    {
        public virtual Order Order { get; set; } = null!;
        public virtual IEnumerable<MenuItem> MenuItemsOrder { get; set; } = null!;

        protected ValidatedOrderCommand() { }

        public ValidatedOrderCommand(Order order, IEnumerable<MenuItem> menuItemsOrder)
        {
            Order = order;
            MenuItemsOrder = menuItemsOrder;
        }
    }
}