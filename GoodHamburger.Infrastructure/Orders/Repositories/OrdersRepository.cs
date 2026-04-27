using GoodHamburger.Domain.Orders.DTOs;
using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.Orders.IRepositories;
using GoodHamburger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Orders.Repositories
{
    public class OrdersRepository(GoodHamburgerDbContext context) : IOrdersRepository
    {

        public async Task<Order?> InsertAsync(Order order, CancellationToken cancellationToken)
        {
            var insertedOrder = await context.Orders.AddAsync(order, cancellationToken);

            await context.SaveChangesAsync();

            return await context.Orders.Where(o => o.Id == insertedOrder.Entity.Id)
                                        .Include(o => o.OrderStatus)
                                        .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken) =>
            await context.Orders.Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.MenuItem)
                                  .Include(o => o.OrderStatus)
                                 .OrderBy(o => o.OrderStatus.Id)
                                 .ThenByDescending(o => o.CreatedAt)
                                 .ToArrayAsync(cancellationToken);

        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task<OrderSummaryDTO?> GetSummaryAsync(int daysPeriod, CancellationToken cancellationToken)
        {
            DateTime dataLimite = DateTime.Now.Date.AddDays(-daysPeriod);

            return await context.Orders.Where(o => o.CreatedAt >= dataLimite)
                                .GroupBy(o => 1)
                                .Select(o => new OrderSummaryDTO
                                {
                                    TotalOrders = o.Count(),
                                    SubtotalAmount = o.Sum(o => o.Subtotal),
                                    TotalAmount = o.Sum(o => o.TotalAmount),
                                    TotalDiscount = o.Sum(o => o.Discount)
                                })
                                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Order> SoftDeleteAsync(Order order, CancellationToken cancellationToken)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}