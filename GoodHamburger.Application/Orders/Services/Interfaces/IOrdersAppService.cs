using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;

namespace GoodHamburger.Application.Orders.Services.Interfaces
{
    public interface IOrdersAppService
    {
        Task<bool> InsertOrderAndItemsAsync(ValidateOrderDto orderDto, CancellationToken cancellationToken);
        Task<IEnumerable<OrderResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<OrderSummaryResponse> GetSummaryAsync(int periodDays, CancellationToken cancellationToken);
        Task<ValidateOrderDto> ValidateOrderAndCalculateDiscount(OrderInsertRequest request, CancellationToken cancellationToken);
        Task<OrderResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> ValidateAndInsertOrderAndItemsAsync(OrderInsertRequest request, CancellationToken cancellationToken);
    }
}