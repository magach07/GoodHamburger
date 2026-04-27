using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.Utils.Exceptions.Entities;

namespace GoodHamburger.Domain.OrdersStatus.Entities
{
    public class OrderStatus
    {
        public virtual int Id { get; protected set; }
        public virtual string Description { get; protected set; } = null!;

        public virtual ICollection<Order> Orders { get; protected set; } = null!;

        public virtual void SetId(int id)
        {
            id.ThrowInvalidAttributeIf(id => id <= 0, "Id Order Status");
            Id = id;
        }

        public virtual void SetDescription(string description)
        {
            Description = description;
        }
    }
}