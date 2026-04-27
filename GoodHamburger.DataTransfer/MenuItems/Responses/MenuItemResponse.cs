using GoodHamburger.DataTransfer.MenuItemsTypes.Responses;

namespace GoodHamburger.DataTransfer.MenuItems.Responses
{
    public class MenuItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int IdMenuItemType { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; } = 1;

        public virtual MenuItemTypeResponse? MenuItemType { get; set; }
    }
}