using GoodHamburger.Domain.OrdersStatus.Entities;
using GoodHamburger.Domain.OrdersStatus.IRepositories;
using GoodHamburger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.OrdersStatus.Repositories
{
    public class OrdersStatusRepository(GoodHamburgerDbContext context) : IOrdersStatusRepository
    {
        public async Task<OrderStatus?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await context.OrdersStatus.Where(os => os.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}