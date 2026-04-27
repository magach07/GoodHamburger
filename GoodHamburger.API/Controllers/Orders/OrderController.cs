using GoodHamburger.Application.Orders.Services.Interfaces;
using GoodHamburger.Application.ResultPattern;
using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.Orders
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController(IOrdersAppService orderAppService) : BaseController
    {
        ///  <remarks> 
        /// Insert an order
        ///  </remarks> 
        /// <param name="request">An order and itens then</param>
        /// <param name="cancellationToken">Token cancellation</param>
        [HttpPost]
        public async Task<ActionResult<bool>> InsertAsync([FromBody] ValidateOrderDto request, CancellationToken cancellationToken)
        {
            AppResult<bool> result = await orderAppService.InsertOrderAndItemsAsync(request, cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// It receives a list of items and validates them.
        ///  </remarks> 
        /// <param name="request">Customer Name and a list with id itens</param>
        /// <param name="cancellationToken">Token cancellation</param>
        /// <response code="200">Returns an object with order informations and object items them</response>
        [HttpPost("validate")]
        public async Task<ActionResult<ValidateOrderDto>> ValidateAsync([FromBody] OrderInsertRequest request, CancellationToken cancellationToken)
        {
            AppResult<ValidateOrderDto> result = await orderAppService.ValidateOrderAndCalculateDiscount(request, cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// GET all Orders
        ///  </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            AppResult<IEnumerable<OrderResponse>> result = await orderAppService.GetAllAsync(cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// GET a summary about ordens for a given period of days
        ///  </remarks> 
        /// <param name="periodDays">Period of days</param>
        /// <param name="cancellationToken">Token cancellation</param>
        [HttpGet("summary")]
        public async Task<ActionResult<OrderSummaryResponse>> GetSummaryAsync([FromQuery] int periodDays, CancellationToken cancellationToken)
        {
            AppResult<OrderSummaryResponse> result = await orderAppService.GetSummaryAsync(periodDays, cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// Soft delete in a specific order
        ///  </remarks> 
        /// <param name="id">Order ID</param>
        /// <param name="cancellationToken">Token cancellation</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> SoftDeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {

            AppResult<OrderResponse> result = await orderAppService.SoftDeleteAsync(id, cancellationToken);
            return HandleResult(result);
        }

        ///  <remarks> 
        /// This endpoint its not would exists. Its exist just to test a complet insertio processe in the backend
        ///  </remarks> 
        [HttpPost("validate-insert")]
        public async Task<ActionResult<bool>> ValidateAndInsertAsync([FromBody] OrderInsertRequest request, CancellationToken cancellationToken)
        {
            AppResult<bool> result = await orderAppService.ValidateAndInsertOrderAndItemsAsync(request, cancellationToken);
            return HandleResult(result);
        }
    }
}