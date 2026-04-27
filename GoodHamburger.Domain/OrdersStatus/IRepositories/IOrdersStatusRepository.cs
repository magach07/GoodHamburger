using GoodHamburger.Domain.OrdersStatus.Entities;

namespace GoodHamburger.Domain.OrdersStatus.IRepositories
{
    public interface IOrdersStatusRepository
    {
        Task<OrderStatus?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}