using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrderItems.IRepositories;
using GoodHamburger.Domain.Orders.Commands;
using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.Orders.IRepositories;
using GoodHamburger.Domain.Orders.Services;
using GoodHamburger.Domain.OrdersStatus.Entities;
using GoodHamburger.Domain.OrdersStatus.IRepositories;
using Moq;

namespace GoodHamburger.Domain.Tests.Orders.Services;

public class OrdersServiceTests
{
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock = new();
    private readonly Mock<IOrderItemsRepository> _orderItemsRepositoryMock = new();
    private readonly Mock<IOrdersStatusRepository> _orderStatusRepositoryMock = new();

    private readonly OrdersService _sut;

    public OrdersServiceTests()
    {
        _sut = new OrdersService(
            _ordersRepositoryMock.Object,
            _orderItemsRepositoryMock.Object,
            _orderStatusRepositoryMock.Object
        );
    }

    [Fact]
    public void ValidateItems_ShouldReturnDuplicatedItems_WhenQuantityIsGreaterThanOne()
    {
        var items = new List<OrderItemsQuantityInsertDTO>
        {
            new() { Id = 1, Quantity = 2 },
            new() { Id = 2, Quantity = 1 },
            new() { Id = 3, Quantity = 3 }
        };

        var result = _sut.ValidateItems(items);

        Assert.Equal([1, 3], result);
    }

    [Fact]
    public void ValidateItems_ShouldReturnEmptyList_WhenThereAreNoDuplicatedItems()
    {
        var items = new List<OrderItemsQuantityInsertDTO>
        {
            new() { Id = 1, Quantity = 1 },
            new() { Id = 2, Quantity = 1 }
        };

        var result = _sut.ValidateItems(items);

        Assert.Empty(result);
    }

    [Fact]
    public async Task ValidateOrderAsync_ShouldApplyTwentyPercentDiscount_WhenOrderHasSandwichFriesAndDrink()
    {
        var orderStatus = CreateOrderStatus(1, "Solicitado");

        _orderStatusRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderStatus);

        var menuItems = new List<MenuItem>
        {
            CreateMenuItem(1, "X Burger", 5.00m, 1),
            CreateMenuItem(4, "Batata frita", 2.00m, 2),
            CreateMenuItem(5, "Refrigerante", 2.50m, 3)
        };

        var command = new OrderInsertCommand("Jonathan", menuItems);

        var result = await _sut.ValidateOrderAsync(command, CancellationToken.None);

        Assert.Equal(9.50m, result.Subtotal);
        Assert.Equal(1.90m, result.Discount);
        Assert.Equal(7.60m, result.TotalAmount);
    }

    [Fact]
    public async Task ValidateOrderAsync_ShouldApplyFifteenPercentDiscount_WhenOrderHasSandwichAndDrink()
    {
        var orderStatus = CreateOrderStatus(1, "Solicitado");

        _orderStatusRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderStatus);

        var menuItems = new List<MenuItem>
        {
            CreateMenuItem(1, "X Burger", 5.00m, 1),
            CreateMenuItem(5, "Refrigerante", 2.50m, 3)
        };

        var command = new OrderInsertCommand("Jonathan", menuItems);

        var result = await _sut.ValidateOrderAsync(command, CancellationToken.None);

        Assert.Equal(7.50m, result.Subtotal);
        Assert.Equal(1.125m, result.Discount);
        Assert.Equal(6.375m, result.TotalAmount);
    }

    [Fact]
    public async Task ValidateOrderAsync_ShouldApplyTenPercentDiscount_WhenOrderHasSandwichAndFries()
    {
        var orderStatus = CreateOrderStatus(1, "Solicitado");

        _orderStatusRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderStatus);

        var menuItems = new List<MenuItem>
        {
            CreateMenuItem(1, "X Burger", 5.00m, 1),
            CreateMenuItem(4, "Batata frita", 2.00m, 2)
        };

        var command = new OrderInsertCommand("Jonathan", menuItems);

        var result = await _sut.ValidateOrderAsync(command, CancellationToken.None);

        Assert.Equal(7.00m, result.Subtotal);
        Assert.Equal(0.70m, result.Discount);
        Assert.Equal(6.30m, result.TotalAmount);
    }

    [Fact]
    public async Task ValidateOrderAsync_ShouldNotApplyDiscount_WhenOrderHasOnlySandwich()
    {
        var orderStatus = CreateOrderStatus(1, "Solicitado");

        _orderStatusRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderStatus);

        var menuItems = new List<MenuItem>
        {
            CreateMenuItem(1, "X Burger", 5.00m, 1)
        };

        var command = new OrderInsertCommand("Jonathan", menuItems);

        var result = await _sut.ValidateOrderAsync(command, CancellationToken.None);

        Assert.Equal(5.00m, result.Subtotal);
        Assert.Equal(0m, result.Discount);
        Assert.Equal(5.00m, result.TotalAmount);
    }

    [Fact]
    public async Task InsertOrderAndItemsAsync_ShouldInsertOrderAndOrderItems()
    {
        var order = new Order("Jonathan", 1);

        var menuItems = new List<MenuItem>
    {
        CreateMenuItem(1, "X Burger", 5.00m, 1),
        CreateMenuItem(5, "Refrigerante", 2.50m, 3)
    };

        var command = new ValidatedOrderCommand(order, menuItems);

        _ordersRepositoryMock
            .Setup(x => x.InsertAsync(order, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        _orderItemsRepositoryMock
            .Setup(x => x.InsertEnumerableAsync(
                It.IsAny<IEnumerable<OrderItem>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _sut.InsertOrderAndItemsAsync(command, CancellationToken.None);

        Assert.True(result);

        _ordersRepositoryMock.Verify(
            x => x.InsertAsync(order, It.IsAny<CancellationToken>()),
            Times.Once);

        _orderItemsRepositoryMock.Verify(
            x => x.InsertEnumerableAsync(
                It.Is<IEnumerable<OrderItem>>(items => items.Count() == 2),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnEmptySummary_WhenRepositoryReturnsNull()
    {
        _ordersRepositoryMock
            .Setup(x => x.GetSummaryAsync(7, It.IsAny<CancellationToken>()))
            .ReturnsAsync((OrderSummaryDTO?)null);

        var result = await _sut.GetSummaryAsync(7, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(0, result.TotalOrders);
        Assert.Equal(0, result.SubtotalAmount);
        Assert.Equal(0, result.TotalAmount);
        Assert.Equal(0, result.TotalDiscount);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldChangeOrderStatusToCanceled()
    {
        var order = new Order("Jonathan", 1);
        var canceledStatus = CreateOrderStatus(3, "Cancelado");

        _ordersRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        _orderStatusRepositoryMock
            .Setup(x => x.GetByIdAsync(3, It.IsAny<CancellationToken>()))
            .ReturnsAsync(canceledStatus);

        _ordersRepositoryMock
            .Setup(x => x.SoftDeleteAsync(order, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var result = await _sut.SoftDeleteAsync(1, CancellationToken.None);

        Assert.Equal(3, result.IdOrderStatus);

        _ordersRepositoryMock.Verify(
            x => x.SoftDeleteAsync(order, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static OrderStatus CreateOrderStatus(int id, string description)
    {
        var status = new OrderStatus();

        status.SetId(id);
        status.SetDescription(description);

        return status;
    }

    private static MenuItem CreateMenuItem(int id, string name, decimal price, int idMenuItemType)
    {
        var menuItem = new MenuItem(name, price, idMenuItemType);
        menuItem.SetId(id);

        return menuItem;
    }
}