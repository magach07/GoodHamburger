using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.UnitTests.Domain.Orders.Entities
{
    public class OrderTests
    {
        [Fact]
        public void Constructor_ShouldCreateOrder_WhenValidData()
        {
            // Arrange
            string customerName = "Jonathan";
            int idOrderStatus = 1;

            // Act
            var order = new Order(customerName, idOrderStatus);

            // Assert
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
            // Arrange
            int idOrderStatus = 1;

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => new Order(customerName, idOrderStatus));
        }

        [Fact]
        public void Constructor_ShouldCreateOrder_WhenOrderStatusIdIsZero_BecauseNoExceptionIsThrown()
        {
            // Arrange
            string customerName = "Jonathan";
            int idOrderStatus = 0;

            // Act
            var order = new Order(customerName, idOrderStatus);

            // Assert
            Assert.Equal(idOrderStatus, order.IdOrderStatus);
        }

        [Fact]
        public void SetCustomerName_ShouldUpdateCustomerName_WhenValid()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetCustomerName("Carlos");

            // Assert
            Assert.Equal("Carlos", order.CustomerName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetCustomerName_ShouldThrowException_WhenInvalid(string customerName)
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => order.SetCustomerName(customerName));
        }

        [Fact]
        public void SetSubtotal_ShouldUpdateSubtotal_WhenValid()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetSubtotal(50);

            // Assert
            Assert.Equal(50, order.Subtotal);
        }

        [Fact]
        public void SetSubtotal_ShouldThrowException_WhenNegative()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => order.SetSubtotal(-1));
        }

        [Fact]
        public void SetDiscount_ShouldUpdateDiscount_WhenValid()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetDiscount(10);

            // Assert
            Assert.Equal(10, order.Discount);
        }

        [Fact]
        public void SetDiscount_ShouldThrowException_WhenNegative()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => order.SetDiscount(-1));
        }

        [Fact]
        public void SetTotalAmount_ShouldUpdateTotalAmount_WhenValid()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetTotalAmount(40);

            // Assert
            Assert.Equal(40, order.TotalAmount);
        }

        [Fact]
        public void SetTotalAmount_ShouldThrowException_WhenNegative()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => order.SetTotalAmount(-1));
        }

        [Fact]
        public void SetIdOrderStatus_ShouldUpdateIdOrderStatus_WhenValid()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetIdOrderStatus(2);

            // Assert
            Assert.Equal(2, order.IdOrderStatus);
        }

        [Fact]
        public void SetIdOrderStatus_ShouldUpdateIdOrderStatus_WhenZero_BecauseNoExceptionIsThrown()
        {
            // Arrange
            var order = new Order("Jonathan", 1);

            // Act
            order.SetIdOrderStatus(0);

            // Assert
            Assert.Equal(0, order.IdOrderStatus);
        }

        [Fact]
        public void SetCreatedAt_ShouldUpdateCreatedAt()
        {
            // Arrange
            var order = new Order("Jonathan", 1);
            var date = new DateTime(2026, 04, 27, 10, 30, 00);

            // Act
            order.SetCreatedAt(date);

            // Assert
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