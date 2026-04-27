using GoodHamburger.DataTransfer.MenuItems.Responses;

namespace GoodHamburger.Application.MenuItems.Services.Interfaces
{
    public interface IMenuItemsAppService
    {
        Task<IEnumerable<MenuItemResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<MenuItemResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}