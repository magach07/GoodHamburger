using GoodHamburger.Application.Orders.Services;
using GoodHamburger.Domain.Orders.Services;
using GoodHamburger.Domain.Utils.Attributes;
using GoodHamburger.Infrastructure.Orders.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.IoC.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddInterfaces(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<OrdersAppService>()
                .AddClasses(classes => classes.WithoutAttribute<IgnoreDependencyInjectionAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<OrdersService>()
                .AddClasses(classes => classes.WithoutAttribute<IgnoreDependencyInjectionAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<OrdersRepository>()
                .AddClasses(classes => classes.WithoutAttribute<IgnoreDependencyInjectionAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}