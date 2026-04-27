namespace GoodHamburger.DataTransfer.MenuItems.Requests
{
    public class MenuItemRequest
    {
        public virtual string Name { get; protected set; } = null!;
        public virtual decimal Price { get; protected set; }
        public virtual int IdMenuItemType { get; protected set; }
        public virtual bool IsAvailavle { get; protected set; }
    }
}