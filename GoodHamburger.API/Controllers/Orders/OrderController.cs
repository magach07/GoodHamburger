using GoodHamburger.Application.Orders.Services.Interfaces;
using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers.Orders
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController(IOrdersAppService orderAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<bool>> InsertAsync([FromBody] ValidateOrderDto request, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await orderAppService.InsertOrderAndItemsAsync(request, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("validate")]
        public async Task<ActionResult<ValidateOrderDto>> ValidateAsync([FromBody] OrderInsertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ValidateOrderDto result = await orderAppService.ValidateOrderAndCalculateDiscount(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OrderResponse> result = await orderAppService.GetAllAsync(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("summary")]
        public async Task<ActionResult<OrderSummaryResponse>> GetSummaryAsync([FromQuery] int periodDays, CancellationToken cancellationToken)
        {
            try
            {
                OrderSummaryResponse result = await orderAppService.GetSummaryAsync(periodDays, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> SoftDeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                OrderResponse result = await orderAppService.SoftDeleteAsync(id, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Esse endpoint não existiria. Existe apenas para disponibilizar endpoint teste de validação e inserção completa via Backend
        [HttpPost("validate-insert")]
        public async Task<ActionResult<bool>> ValidateAndInsertAsync([FromBody] OrderInsertRequest request, CancellationToken cancellationToken) 
        {
            try
            {
                return Ok(await orderAppService.ValidateAndInsertOrderAndItemsAsync(request, cancellationToken));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}