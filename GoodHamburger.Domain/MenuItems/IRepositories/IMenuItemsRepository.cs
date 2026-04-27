using GoodHamburger.Domain.MenuItems.Entities;

namespace GoodHamburger.Domain.MenuItems.IRepositories
{
    public interface IMenuItemsRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<MenuItem>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    }
}