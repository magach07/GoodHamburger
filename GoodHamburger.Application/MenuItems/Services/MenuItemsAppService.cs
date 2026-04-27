using GoodHamburger.Application.MenuItems.Services.Interfaces;
using GoodHamburger.DataTransfer.MenuItems.Responses;
using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.MenuItems.Services.Interfaces;
using Mapster;

namespace GoodHamburger.Application.MenuItems.Services
{
    public class MenuItemsAppService : IMenuItemsAppService
    {
        private readonly IMenuItemsService _menuItemService;

        public MenuItemsAppService(IMenuItemsService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public async Task<IEnumerable<MenuItemResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<MenuItem> menuItems = await _menuItemService.GetAllAsync(cancellationToken);

                return menuItems.Adapt<IEnumerable<MenuItemResponse>>();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<MenuItemResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                MenuItem menuItem = await _menuItemService.GetByIdAsync(id, cancellationToken);

                return menuItem.Adapt<MenuItemResponse>();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
