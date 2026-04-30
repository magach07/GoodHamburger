using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.UnitTests.Domain.MenuItems.Entities
{
    public class MenuItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateMenuItem_WhenValidData()
        {
            string name = "Hamburguer";
            int price = 25;
            int typeId = 1;

            var menuItem = new MenuItem(name, price, typeId);

            Assert.Equal(name, menuItem.Name);
            Assert.Equal(price, menuItem.Price);
            Assert.Equal(typeId, menuItem.IdMenuItemType);
        }

        [Fact]
        public void Constructor_ShouldAllowZeroPrice()
        {
            string name = "Água";
            int price = 0;
            int typeId = 1;

            var menuItem = new MenuItem(name, price, typeId);

            Assert.Equal(0, menuItem.Price);
        }

        [Fact]
        public void Constructor_ShouldAllowNegativePrice_BecauseNoValidationExists()
        {
            string name = "Item inválido";
            int price = -10;
            int typeId = 1;

            var menuItem = new MenuItem(name, price, typeId);

            Assert.Equal(-10, menuItem.Price);
        }

        [Fact]
        public void Constructor_ShouldAllowEmptyName_BecauseNoValidationExists()
        {
            string name = "";
            int price = 10;
            int typeId = 1;

            var menuItem = new MenuItem(name, price, typeId);

            Assert.Equal(name, menuItem.Name);
        }

        [Fact]
        public void Constructor_ShouldAllowNullName_BecauseNoValidationExists()
        {
            string name = null!;
            int price = 10;
            int typeId = 1;

            var menuItem = new MenuItem(name, price, typeId);

            Assert.Null(menuItem.Name);
        }

        [Fact]
        public void IsAvailable_ShouldBeFalseByDefault()
        {
            var menuItem = new MenuItem("Hamburguer", 20, 1);

            Assert.False(menuItem.IsAvailable);
        }
    }
}