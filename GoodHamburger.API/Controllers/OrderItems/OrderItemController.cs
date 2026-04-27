using GoodHamburger.Application.OrderItems.Services.Interfaces;
using GoodHamburger.DataTransfer.OrderItems.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.OrderItems
{
    [ApiController]
    [Route("api/order-items")]
    public class OrderItemController (IOrderItemsAppService orderItemAppService): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAllAsync(CancellationToken cancellationToken) =>
            Ok(await orderItemAppService.GetAllAsync(cancellationToken));

        [HttpGet("most-solds")]
        public async Task<ActionResult<IEnumerable<OrderItemMostSoldItemsRankingResponse>>> GetMostSoldItemsAsync(CancellationToken cancellationToken) =>
            Ok(await orderItemAppService.GetMostSoldItemsAsync(cancellationToken));
    }
}