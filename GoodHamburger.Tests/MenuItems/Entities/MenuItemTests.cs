using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.UnitTests.Domain.MenuItems.Entities
{
    public class MenuItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateMenuItem_WhenValidData()
        {
            // Arrange
            string name = "Hamburguer";
            int price = 25;
            int typeId = 1;

            // Act
            var menuItem = new MenuItem(name, price, typeId);

            // Assert
            Assert.Equal(name, menuItem.Name);
            Assert.Equal(price, menuItem.Price);
            Assert.Equal(typeId, menuItem.IdMenuItemType);
        }

        [Fact]
        public void Constructor_ShouldAllowZeroPrice()
        {
            // Arrange
            string name = "Água";
            int price = 0;
            int typeId = 1;

            // Act
            var menuItem = new MenuItem(name, price, typeId);

            // Assert
            Assert.Equal(0, menuItem.Price);
        }

        [Fact]
        public void Constructor_ShouldAllowNegativePrice_BecauseNoValidationExists()
        {
            // Arrange
            string name = "Item inválido";
            int price = -10;
            int typeId = 1;

            // Act
            var menuItem = new MenuItem(name, price, typeId);

            // Assert
            Assert.Equal(-10, menuItem.Price);
        }

        [Fact]
        public void Constructor_ShouldAllowEmptyName_BecauseNoValidationExists()
        {
            // Arrange
            string name = "";
            int price = 10;
            int typeId = 1;

            // Act
            var menuItem = new MenuItem(name, price, typeId);

            // Assert
            Assert.Equal(name, menuItem.Name);
        }

        [Fact]
        public void Constructor_ShouldAllowNullName_BecauseNoValidationExists()
        {
            // Arrange
            string name = null!;
            int price = 10;
            int typeId = 1;

            // Act
            var menuItem = new MenuItem(name, price, typeId);

            // Assert
            Assert.Null(menuItem.Name);
        }

        [Fact]
        public void IsAvailable_ShouldBeFalseByDefault()
        {
            // Arrange & Act
            var menuItem = new MenuItem("Hamburguer", 20, 1);

            // Assert
            Assert.False(menuItem.IsAvailable);
        }
    }
}