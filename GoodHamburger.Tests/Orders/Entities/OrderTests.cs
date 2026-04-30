using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.UnitTests.Domain.Orders.Entities
{
    public class OrderTests
    {
        [Fact]
        public void Constructor_ShouldCreateOrder_WhenValidData()
        {
            string customerName = "Jonathan";
            int idOrderStatus = 1;

            var order = new Order(customerName, idOrderStatus);

            Assert.Equal(customerName, order.CustomerName);
            Assert.Equal(idOrderStatus, order.IdOrderStatus);
            Assert.True(order.CreatedAt > DateTime.MinValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_ShouldThrowException_WhenCustomerNameIsInvalid(string customerName)
        {
            int idOrderStatus = 1;

            Assert.ThrowsAny<Exception>(() => new Order(customerName, idOrderStatus));
        }

        [Fact]
        public void Constructor_ShouldCreateOrder_WhenOrderStatusIdIsZero_BecauseNoExceptionIsThrown()
        {
            string customerName = "Jonathan";
            int idOrderStatus = 0;

            var order = new Order(customerName, idOrderStatus);

            Assert.Equal(idOrderStatus, order.IdOrderStatus);
        }

        [Fact]
        public void SetCustomerName_ShouldUpdateCustomerName_WhenValid()
        {
            var order = new Order("Jonathan", 1);

            order.SetCustomerName("Carlos");

            Assert.Equal("Carlos", order.CustomerName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetCustomerName_ShouldThrowException_WhenInvalid(string customerName)
        {
            var order = new Order("Jonathan", 1);

            Assert.ThrowsAny<Exception>(() => order.SetCustomerName(customerName));
        }

        [Fact]
        public void SetSubtotal_ShouldUpdateSubtotal_WhenValid()
        {
            var order = new Order("Jonathan", 1);

            order.SetSubtotal(50);

            Assert.Equal(50, order.Subtotal);
        }

        [Fact]
        public void SetSubtotal_ShouldThrowException_WhenNegative()
        {
            var order = new Order("Jonathan", 1);

            Assert.ThrowsAny<Exception>(() => order.SetSubtotal(-1));
        }

        [Fact]
        public void SetDiscount_ShouldUpdateDiscount_WhenValid()
        {
            var order = new Order("Jonathan", 1);

            order.SetDiscount(10);

            Assert.Equal(10, order.Discount);
        }

        [Fact]
        public void SetDiscount_ShouldThrowException_WhenNegative()
        {
            var order = new Order("Jonathan", 1);

            Assert.ThrowsAny<Exception>(() => order.SetDiscount(-1));
        }

        [Fact]
        public void SetTotalAmount_ShouldUpdateTotalAmount_WhenValid()
        {
            var order = new Order("Jonathan", 1);

            order.SetTotalAmount(40);

            Assert.Equal(40, order.TotalAmount);
        }

        [Fact]
        public void SetTotalAmount_ShouldThrowException_WhenNegative()
        {
            var order = new Order("Jonathan", 1);

            Assert.ThrowsAny<Exception>(() => order.SetTotalAmount(-1));
        }

        [Fact]
        public void SetIdOrderStatus_ShouldUpdateIdOrderStatus_WhenValid()
        {
            var order = new Order("Jonathan", 1);

            order.SetIdOrderStatus(2);

            Assert.Equal(2, order.IdOrderStatus);
        }

        [Fact]
        public void SetIdOrderStatus_ShouldUpdateIdOrderStatus_WhenZero_BecauseNoExceptionIsThrown()
        {
            var order = new Order("Jonathan", 1);

            order.SetIdOrderStatus(0);

            Assert.Equal(0, order.IdOrderStatus);
        }

        [Fact]
        public void SetCreatedAt_ShouldUpdateCreatedAt()
        {
            var order = new Order("Jonathan", 1);
            var date = new DateTime(2026, 04, 27, 10, 30, 00);

            order.SetCreatedAt(date);

            Assert.Equal(date, order.CreatedAt);
        }

        [Fact]
        public void SetUpdatedAt_ShouldUpdateUpdatedAt()
        {
            var order = new Order("Jonathan", 1);
            var date = new DateTime(2026, 04, 27, 10, 30, 00);

            order.SetUpdatedAt(date);

            Assert.Equal(date, order.UpdatedAt);
        }
    }
}