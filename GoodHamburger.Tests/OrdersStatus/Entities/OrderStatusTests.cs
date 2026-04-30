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
            var orderStatus = CreateInstance();

            Assert.NotNull(orderStatus);
        }

        [Fact]
        public void ShouldSetId_ViaReflection()
        {
            var orderStatus = CreateInstance();
            int id = 1;

            typeof(OrderStatus)
                .GetProperty(nameof(OrderStatus.Id))!
                .SetValue(orderStatus, id);

            Assert.Equal(id, orderStatus.Id);
        }

        [Fact]
        public void ShouldSetDescription_ViaReflection()
        {
            var orderStatus = CreateInstance();
            string description = "Solicitado";

            typeof(OrderStatus)
                .GetProperty(nameof(OrderStatus.Description))!
                .SetValue(orderStatus, description);

            Assert.Equal(description, orderStatus.Description);
        }

        [Fact]
        public void Orders_ShouldBeNull_ByDefault()
        {
            var orderStatus = CreateInstance();

            Assert.Null(orderStatus.Orders);
        }
    }
}