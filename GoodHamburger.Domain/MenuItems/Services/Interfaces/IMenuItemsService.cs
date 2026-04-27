using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.Domain.MenuItems.Services.Interfaces
{
    public interface IMenuItemsService
    {
        Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<MenuItem> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}