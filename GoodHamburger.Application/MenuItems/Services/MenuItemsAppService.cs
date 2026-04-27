using GoodHamburger.Application.MenuItems.Services.Interfaces;
using GoodHamburger.Application.ResultPattern;
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

        public async Task<AppResult<IEnumerable<MenuItemResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<MenuItem> menuItems = await _menuItemService.GetAllAsync(cancellationToken);

                return AppResult<IEnumerable<MenuItemResponse>>.Ok(menuItems.Adapt<IEnumerable<MenuItemResponse>>());
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<MenuItemResponse>>.Fail(ex.Message);
            }
        }

        public async Task<AppResult<MenuItemResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                MenuItem menuItem = await _menuItemService.GetByIdAsync(id, cancellationToken);

                return AppResult<MenuItemResponse>.Ok(menuItem.Adapt<MenuItemResponse>());
            }
            catch (Exception ex)
            {
                return AppResult<MenuItemResponse>.Fail(ex.Message);
            }
        }
    }
}
