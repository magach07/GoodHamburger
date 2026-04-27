using GoodHamburger.DataTransfer.MenuItems.Responses;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWASM.MenuItems.Services
{
    public class MenuItemService (HttpClient httpClient)
    {
        public async Task<IEnumerable<MenuItemResponse>> GetAllAsync(CancellationToken cancellationToken) =>
            await httpClient.GetFromJsonAsync<IEnumerable<MenuItemResponse>>("api/menu-items", cancellationToken) ?? [];
    }
}