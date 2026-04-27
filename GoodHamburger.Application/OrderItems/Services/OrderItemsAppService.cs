using GoodHamburger.Application.OrderItems.Services.Interfaces;
using GoodHamburger.DataTransfer.OrderItems.Responses;
using GoodHamburger.Domain.OrderItems.DTOs;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.OrderItems.Services.Interfaces;
using Mapster;

namespace GoodHamburger.Application.OrderItems.Services
{
    public class OrderItemsAppService (IOrderItemsService orderItemsService) : IOrderItemsAppService
    {
        public async Task<IEnumerable<OrderItemResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OrderItem> orderItems = await orderItemsService.GetAllAsync(cancellationToken);
                return orderItems.Adapt<IEnumerable<OrderItemResponse>>();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderItemMostSoldItemsRankingResponse>> GetMostSoldItemsAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OrderItemMostSoldItemsRankingDTO> mostSoldItems = await orderItemsService.GetMostSoldItemsAsync(cancellationToken);
                return mostSoldItems.Adapt<IEnumerable<OrderItemMostSoldItemsRankingResponse>>();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}