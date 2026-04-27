using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.Domain.MenuItemsTypes.Entitites
{
    public class MenuItemType
    {
        public virtual int Id { get; protected set; }
        public virtual string Description { get; protected set; } = null!;

        public virtual ICollection<MenuItem> MenuItems { get; protected set; }
    }
}
