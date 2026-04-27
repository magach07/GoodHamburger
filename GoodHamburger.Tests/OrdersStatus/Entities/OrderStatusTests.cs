using GoodHamburger.Domain.OrdersStatus.Entities;

namespace GoodHamburger.UnitTests.Domain.OrdersStatus.Entities
{
    public class OrderStatusTests
    {
        private static OrderStatus CreateInstance()
        {
            return (OrderStatus)Activator.CreateInstance(typeof(OrderStatus), true)!;
        }

        [Fact]
        public void ShouldCreateInstance_WithProtectedConstructor()
        {
            // Act
            var orderStatus = CreateInstance();

            // Assert
            Assert.NotNull(orderStatus);
        }

        [Fact]
        public void ShouldSetId_ViaReflection()
        {
            // Arrange
            var orderStatus = CreateInstance();
            int id = 1;

            // Act
            typeof(OrderStatus)
                .GetProperty(nameof(OrderStatus.Id))!
                .SetValue(orderStatus, id);

            // Assert
            Assert.Equal(id, orderStatus.Id);
        }

        [Fact]
        public void ShouldSetDescription_ViaReflection()
        {
            // Arrange
            var orderStatus = CreateInstance();
            string description = "Solicitado";

            // Act
            typeof(OrderStatus)
                .GetProperty(nameof(OrderStatus.Description))!
                .SetValue(orderStatus, description);

            // Assert
            Assert.Equal(description, orderStatus.Description);
        }

        [Fact]
        public void Orders_ShouldBeNull_ByDefault()
        {
            // Arrange
            var orderStatus = CreateInstance();

            // Assert
            Assert.Null(orderStatus.Orders);
        }
    }
}