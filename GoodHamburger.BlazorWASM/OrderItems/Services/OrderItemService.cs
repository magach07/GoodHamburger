using GoodHamburger.DataTransfer.OrderItems.Responses;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWASM.OrderItems.Services
{
    public class OrderItemService (HttpClient httpClient)
    {
        public async Task<IEnumerable<OrderItemMostSoldItemsRankingResponse>> GetMostSoldItemsAsync(CancellationToken cancellationToken) =>
            await httpClient.GetFromJsonAsync<IEnumerable<OrderItemMostSoldItemsRankingResponse>>("api/order-items/most-solds", cancellationToken) ?? [];
    }
}
