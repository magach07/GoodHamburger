using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.Domain.OrderItems.Entitites
{
    public class OrderItem
    {
        public virtual int Id { get; protected set; }
        public virtual int IdOrder { get; protected set; }
        public virtual int IdMenuItem { get; protected set; }

        public virtual Order Order { get; protected set; } = null!;
        public virtual MenuItem MenuItem { get; protected set; } = null!;

        protected OrderItem() { }
        public OrderItem(Order order, MenuItem menuItem)
        {
            SetOrder(order);
            SetMenuItem(menuItem);
        }

        public virtual void SetOrder(Order order)
        {
            IdOrder = order.Id;
        }

        public virtual void SetMenuItem(MenuItem menuItem)
        { 
            IdMenuItem = menuItem.Id; 
        }
    }
}