using GoodHamburger.Application.OrderItems.Services.Interfaces;
using GoodHamburger.DataTransfer.OrderItems.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.OrderItems
{
    [ApiController]
    [Route("api/order-items")]
    public class OrderItemController(IOrderItemsAppService orderItemAppService) : ControllerBase
    {
        ///  <remarks> 
        /// Returns all OrderItem
        ///  </remarks> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAllAsync(CancellationToken cancellationToken) =>
            Ok(await orderItemAppService.GetAllAsync(cancellationToken));

        ///  <remarks> 
        /// Returns the best-selling items in an organized manner.
        ///  </remarks> 
        [HttpGet("most-solds")]
        public async Task<ActionResult<IEnumerable<OrderItemMostSoldItemsRankingResponse>>> GetMostSoldItemsAsync(CancellationToken cancellationToken) =>
            Ok(await orderItemAppService.GetMostSoldItemsAsync(cancellationToken));
    }
}