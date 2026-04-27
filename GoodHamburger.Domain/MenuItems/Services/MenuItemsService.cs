using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.MenuItems.IRepositories;
using GoodHamburger.Domain.MenuItems.Services.Interfaces;
using GoodHamburger.Domain.Utils.Exceptions.Entities;

namespace GoodHamburger.Domain.MenuItems.Services
{
    public class MenuItemsService : IMenuItemsService
    {
        public readonly IMenuItemsRepository _menuItemRepository;

        public MenuItemsService(IMenuItemsRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync(CancellationToken cancellationToken) =>
            await _menuItemRepository.GetAllAsync(cancellationToken);

        public async Task<MenuItem> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            MenuItem? menuItem = await _menuItemRepository.GetByIdAsync(id, cancellationToken);
            menuItem.ThrowRecordNotFound("MenuItem");
            return menuItem!;
        }
    }
}