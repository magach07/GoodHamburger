using GoodHamburger.Domain.OrderItems.DTOs;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrderItems.IRepositories;
using GoodHamburger.Domain.OrderItems.Services.Interfaces;

namespace GoodHamburger.Domain.OrderItems.Services
{
    public class OrderItemsService(IOrderItemsRepository orderItemRepository) : IOrderItemsService
    {
        public async Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken) =>
            await orderItemRepository.GetAllAsync(cancellationToken);

        public async Task<IEnumerable<OrderItemMostSoldItemsRankingDTO>> GetMostSoldItemsAsync(CancellationToken cancellationToken) =>
            await orderItemRepository.GetMostSoldItemsAsync(cancellationToken);
    }
}