using GoodHamburger.Domain.Orders.Commands;
using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.Domain.Orders.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<bool> ValidateAndInsertOrderAndItemsAsync(OrderInsertCommand command, CancellationToken cancellationToken);
        Task<Order> ValidateOrderAsync(OrderInsertCommand command, CancellationToken cancellationToken);
        Task<bool> InsertOrderAndItemsAsync(ValidatedOrderCommand command, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<OrderSummaryDTO> GetSummaryAsync(int pediodDays, CancellationToken cancellationToken);
        List<int> ValidateItems(IEnumerable<OrderItemsQuantityInsertDTO> orderItemsQuantity);
        Task<Order> SoftDeleteAsync(int id, CancellationToken cancellationToken);
    }
}