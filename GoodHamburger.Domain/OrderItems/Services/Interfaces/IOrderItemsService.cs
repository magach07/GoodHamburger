using GoodHamburger.Domain.OrderItems.DTOs;
using GoodHamburger.Domain.OrderItems.Entitites;

namespace GoodHamburger.Domain.OrderItems.Services.Interfaces
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderItemMostSoldItemsRankingDTO>> GetMostSoldItemsAsync(CancellationToken cancellationToken);
    }
}