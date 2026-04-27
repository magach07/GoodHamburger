using GoodHamburger.Domain.MenuItemsTypes.Entitites;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.Utils.Exceptions.Entities;
using System.Runtime.InteropServices.Marshalling;

namespace GoodHamburger.Domain.MenuItems.Entities
{
    public class MenuItem
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; } = null!;
        public virtual decimal Price { get; protected set; }
        public virtual int IdMenuItemType { get; protected set; }
        public virtual bool IsAvailable { get; protected set; }

        public virtual MenuItemType MenuItemType { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; protected set; } = null!;

        public MenuItem(string name, decimal price, int idMenuItemType)
        {
            Name = name;
            Price = price;
            IdMenuItemType = idMenuItemType;
        }

        public virtual void SetId(int id)
        {
            id.ThrowInvalidAttributeIf(id => id <= 0, "Id Menu Item");
            Id = id;
        }
    }
}