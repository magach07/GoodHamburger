using GoodHamburger.Application.ResultPattern;
using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;

namespace GoodHamburger.Application.Orders.Services.Interfaces
{
    public interface IOrdersAppService
    {
        Task<AppResult<bool>> InsertOrderAndItemsAsync(ValidateOrderDto orderDto, CancellationToken cancellationToken);
        Task<AppResult<IEnumerable<OrderResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<AppResult<OrderSummaryResponse>> GetSummaryAsync(int periodDays, CancellationToken cancellationToken);
        Task<AppResult<ValidateOrderDto>> ValidateOrderAndCalculateDiscount(OrderInsertRequest request, CancellationToken cancellationToken);
        Task<AppResult<OrderResponse>> SoftDeleteAsync(int id, CancellationToken cancellationToken);

        Task<AppResult<bool>> ValidateAndInsertOrderAndItemsAsync(OrderInsertRequest request, CancellationToken cancellationToken);
    }
}