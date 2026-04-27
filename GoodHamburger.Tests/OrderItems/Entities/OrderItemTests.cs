using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.UnitTests.Domain.OrderItems.Entities
{
    public class OrderItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateOrderItem_WhenValidData()
        {
            // Arrange
            var order = new Order("Jonathan", 1);
            var menuItem = CreateMenuItem();

            // Act
            var orderItem = new OrderItem(order, menuItem);

            // Assert
            Assert.Equal(order.Id, orderItem.IdOrder);
            Assert.Equal(menuItem.Id, orderItem.IdMenuItem);
        }

        [Fact]
        public void SetOrder_ShouldUpdateIdOrder()
        {
            // Arrange
            var orderItem = new OrderItem(new Order("Jonathan", 1), CreateMenuItem());
            var newOrder = new Order("Carlos", 1);

            // Act
            orderItem.SetOrder(newOrder);

            // Assert
            Assert.Equal(newOrder.Id, orderItem.IdOrder);
        }

        [Fact]
        public void SetMenuItem_ShouldUpdateIdMenuItem()
        {
            // Arrange
            var orderItem = new OrderItem(new Order("Jonathan", 1), CreateMenuItem());
            var newMenuItem = CreateMenuItem();

            // Act
            orderItem.SetMenuItem(newMenuItem);

            // Assert
            Assert.Equal(newMenuItem.Id, orderItem.IdMenuItem);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenOrderIsNull()
        {
            // Arrange
            var menuItem = CreateMenuItem();

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => new OrderItem(null!, menuItem));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenMenuItemIsNull()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => new OrderItem(order, null!));
        }

        private static MenuItem CreateMenuItem()
        {
            return new MenuItem("Sanduíche", 10, 1);
        }
    }
}