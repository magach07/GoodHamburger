using GoodHamburger.Application.MenuItems.Services.Interfaces;
using GoodHamburger.DataTransfer.MenuItems.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.MenuItems
{
    [ApiController]
    [Route("api/menu-items")]
    public class MenuItemController (IMenuItemsAppService menuItemsAppService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            IEnumerable<MenuItemResponse> menuItems = await menuItemsAppService.GetAllAsync(cancellationToken);
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemResponse>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            MenuItemResponse menuItem = await menuItemsAppService.GetByIdAsync(id, cancellationToken);
            return menuItem;
        }
    }
}
