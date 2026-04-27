using GoodHamburger.Domain.OrderItems.DTOs;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrderItems.IRepositories;
using GoodHamburger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.OrderItems.Repositories
{
    public class OrderItemsRepository(GoodHamburgerDbContext context) : IOrderItemsRepository
    {
        public async Task<bool> InsertEnumerableAsync(IEnumerable<OrderItem> orderItems, CancellationToken cancellationToken)
        {
            await context.AddRangeAsync(orderItems, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken) =>
            await context.OrderItems.OrderByDescending(oi => oi.Id)
                                    .Include(oi => oi.Order)
                                    .Include(oi => oi.MenuItem)
                                    .ToListAsync(cancellationToken);

        public async Task<IEnumerable<OrderItemMostSoldItemsRankingDTO>> GetMostSoldItemsAsync(CancellationToken cancellationToken) =>
            await context.OrderItems.GroupBy(oi => oi.MenuItem)
                                    .Select(g => new OrderItemMostSoldItemsRankingDTO
                                    {
                                        Id = g.Key.Id,
                                        NameItem = g.Key.Name,
                                        Quantity = g.Count()
                                    })
                                    .OrderByDescending(x => x.Quantity)
                                    .ToListAsync(cancellationToken);
    }
}