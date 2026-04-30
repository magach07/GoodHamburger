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
            var menuItemType = CreateInstance();

            Assert.NotNull(menuItemType);
        }

        [Fact]
        public void ShouldSetDescription_ViaReflection()
        {
            var menuItemType = CreateInstance();
            var description = "Sanduíches";

            typeof(MenuItemType)
                .GetProperty(nameof(MenuItemType.Description))!
                .SetValue(menuItemType, description);

            Assert.Equal(description, menuItemType.Description);
        }

        [Fact]
        public void ShouldSetId_ViaReflection()
        {
            var menuItemType = CreateInstance();
            int id = 1;

            typeof(MenuItemType)
                .GetProperty(nameof(MenuItemType.Id))!
                .SetValue(menuItemType, id);

            Assert.Equal(id, menuItemType.Id);
        }

        [Fact]
        public void MenuItems_ShouldBeNull_ByDefault()
        {
            var menuItemType = CreateInstance();

            Assert.Null(menuItemType.MenuItems);
        }
    }
}