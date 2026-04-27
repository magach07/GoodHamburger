using GoodHamburger.Application.MenuItems.Services.Interfaces;
using GoodHamburger.Application.ResultPattern;
using GoodHamburger.DataTransfer.MenuItems.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.MenuItems
{
    [ApiController]
    [Route("api/menu-items")]
    public class MenuItemController(IMenuItemsAppService menuItemsAppService) : BaseController
    {
        ///  <remarks> 
        /// GET all menu items 
        ///  </remarks> 
        ///  <returns> Uma lista de todos os produtos </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            AppResult<IEnumerable<MenuItemResponse>> result = await menuItemsAppService.GetAllAsync(cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// GET a specific menu item 
        ///  </remarks> 
        /// <param name="id">Menu item ID</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemResponse>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            AppResult<MenuItemResponse> result = await menuItemsAppService.GetByIdAsync(id, cancellationToken);
            return HandleResult(result);
        }
    }
}