using GoodHamburger.DataTransfer.OrderItems.Responses;

namespace GoodHamburger.Application.OrderItems.Services.Interfaces
{
    public interface IOrderItemsAppService
    {
        Task<IEnumerable<OrderItemResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderItemMostSoldItemsRankingResponse>> GetMostSoldItemsAsync(CancellationToken cancellationToken);
    }
}