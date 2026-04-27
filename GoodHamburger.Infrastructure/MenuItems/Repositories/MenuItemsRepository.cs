using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.MenuItems.IRepositories;
using GoodHamburger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.MenuItems.Repositories
{
    public class MenuItemsRepository(GoodHamburgerDbContext context) : IMenuItemsRepository
    {

        public async Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken cancellationToken) =>
            await context.MenuItems.Include(mi => mi.MenuItemType)
                                    .ToListAsync(cancellationToken);

        public async Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await context.MenuItems.Where(mi => mi.Id == id)
                                    .Include(mi => mi.MenuItemType)
                                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<MenuItem>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken) =>
            await context.MenuItems.Where(mi => ids.Contains(mi.Id))
                                   .Include(mi => mi.MenuItemType)
                                   .ToListAsync(cancellationToken);
    }
}