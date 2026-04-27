using GoodHamburger.Application.ResultPattern;
using GoodHamburger.DataTransfer.MenuItems.Responses;

namespace GoodHamburger.Application.MenuItems.Services.Interfaces
{
    public interface IMenuItemsAppService
    {
        Task<AppResult<IEnumerable<MenuItemResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<AppResult<MenuItemResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}