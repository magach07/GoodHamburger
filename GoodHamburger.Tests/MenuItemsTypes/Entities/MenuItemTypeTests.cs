using GoodHamburger.Domain.MenuItemsTypes.Entitites;

namespace GoodHamburger.UnitTests.Domain.MenuItemsTypes.Entities
{
    public class MenuItemTypeTests
    {
        private static MenuItemType CreateInstance()
        {
            return (MenuItemType)Activator.CreateInstance(typeof(MenuItemType), true)!;
        }

        [Fact]
        public void ShouldCreateInstance_WithProtectedConstructor()
        {
            // Act
            var menuItemType = CreateInstance();

            // Assert
            Assert.NotNull(menuItemType);
        }

        [Fact]
        public void ShouldSetDescription_ViaReflection()
        {
            // Arrange
            var menuItemType = CreateInstance();
            var description = "Sanduíches";

            // Act
            typeof(MenuItemType)
                .GetProperty(nameof(MenuItemType.Description))!
                .SetValue(menuItemType, description);

            // Assert
            Assert.Equal(description, menuItemType.Description);
        }

        [Fact]
        public void ShouldSetId_ViaReflection()
        {
            // Arrange
            var menuItemType = CreateInstance();
            int id = 1;

            // Act
            typeof(MenuItemType)
                .GetProperty(nameof(MenuItemType.Id))!
                .SetValue(menuItemType, id);

            // Assert
            Assert.Equal(id, menuItemType.Id);
        }

        [Fact]
        public void MenuItems_ShouldBeNull_ByDefault()
        {
            // Arrange
            var menuItemType = CreateInstance();

            // Assert
            Assert.Null(menuItemType.MenuItems);
        }
    }
}