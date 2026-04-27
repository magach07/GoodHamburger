using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;

namespace GoodHamburger.Domain.Orders.IRepositories
{
    public interface IOrdersRepository
    {
        Task<Order?> InsertAsync(Order order, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<OrderSummaryDTO?> GetSummaryAsync(int daysPeriod, CancellationToken cancellationToken);
        Task<Order> SoftDeleteAsync(Order order, CancellationToken cancellationToken);
    }
}