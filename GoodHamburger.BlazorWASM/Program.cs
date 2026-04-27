using GoodHamburger.BlazorWASM;
using GoodHamburger.BlazorWASM.MenuItems.Services;
using GoodHamburger.BlazorWASM.OrderItems.Services;
using GoodHamburger.BlazorWASM.Orders.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<MenuItemService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:44391/")
});

await builder.Build().RunAsync();
