using GoodHamburger.DataTransfer.Order.Requests;
using GoodHamburger.DataTransfer.Orders.DTOs;
using GoodHamburger.DataTransfer.Orders.Responses;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWASM.Orders.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> InsertAsync(ValidateOrderDto request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);

                throw new Exception(error);
            }

            return await response.Content.ReadFromJsonAsync<bool>(cancellationToken);
        }

        public async Task<ValidateOrderDto> ValidateAsync( OrderInsertRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders/validate", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);

                throw new Exception(error);
            }

            return await response.Content.ReadFromJsonAsync<ValidateOrderDto>(cancellationToken)
                   ?? new ValidateOrderDto();
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync(CancellationToken cancellationToken) =>
            await _httpClient.GetFromJsonAsync<IEnumerable<OrderResponse>>("api/orders", cancellationToken) ?? [];

        public async Task<OrderSummaryResponse> GetSummaryAsync(int periodDays, CancellationToken cancellationToken) =>
            await _httpClient.GetFromJsonAsync<OrderSummaryResponse>($"api/orders/summary?periodDays={periodDays}", cancellationToken) ?? new OrderSummaryResponse();
    }
}