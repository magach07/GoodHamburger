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
            var order = new Order("Jonathan", 1);
            var menuItem = CreateMenuItem();

            var orderItem = new OrderItem(order, menuItem);

            Assert.Equal(order.Id, orderItem.IdOrder);
            Assert.Equal(menuItem.Id, orderItem.IdMenuItem);
        }

        [Fact]
        public void SetOrder_ShouldUpdateIdOrder()
        {
            var orderItem = new OrderItem(new Order("Jonathan", 1), CreateMenuItem());
            var newOrder = new Order("Carlos", 1);

            orderItem.SetOrder(newOrder);

            Assert.Equal(newOrder.Id, orderItem.IdOrder);
        }

        [Fact]
        public void SetMenuItem_ShouldUpdateIdMenuItem()
        {
            var orderItem = new OrderItem(new Order("Jonathan", 1), CreateMenuItem());
            var newMenuItem = CreateMenuItem();

            orderItem.SetMenuItem(newMenuItem);

            Assert.Equal(newMenuItem.Id, orderItem.IdMenuItem);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenOrderIsNull()
        {
            var menuItem = CreateMenuItem();

            Assert.ThrowsAny<Exception>(() => new OrderItem(null!, menuItem));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenMenuItemIsNull()
        {
            var order = new Order("Jonathan", 1);

            Assert.ThrowsAny<Exception>(() => new OrderItem(order, null!));
        }

        private static MenuItem CreateMenuItem()
        {
            return new MenuItem("Sanduíche", 10, 1);
        }
    }
}