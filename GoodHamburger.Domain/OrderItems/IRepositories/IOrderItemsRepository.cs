using GoodHamburger.Domain.OrderItems.DTOs;
using GoodHamburger.Domain.OrderItems.Entitites;

namespace GoodHamburger.Domain.OrderItems.IRepositories
{
    public interface IOrderItemsRepository
    {
        Task<bool> InsertEnumerableAsync(IEnumerable<OrderItem> orderItems, CancellationToken cancellationToken);
        Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderItemMostSoldItemsRankingDTO>> GetMostSoldItemsAsync(CancellationToken cancellationToken);
    }
}