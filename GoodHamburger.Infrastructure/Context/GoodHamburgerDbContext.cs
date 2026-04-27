using GoodHamburger.Domain.MenuItems.Entities;
using GoodHamburger.Domain.MenuItemsTypes.Entitites;
using GoodHamburger.Domain.OrderItems.Entitites;
using GoodHamburger.Domain.Orders.Entities;
using GoodHamburger.Domain.OrdersStatus.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Context
{
    public class GoodHamburgerDbContext : DbContext
    {
        public GoodHamburgerDbContext(DbContextOptions<GoodHamburgerDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderStatus> OrdersStatus { get; set; }
        public DbSet<MenuItemType> MenuItemsTypes{ get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GoodHamburgerDbContext).Assembly);
        }
    }
}