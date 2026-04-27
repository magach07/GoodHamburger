using GoodHamburger.Domain.OrdersStatus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.OrdersStatus.Configurations
{
    public class OrdersStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> entity)
        {
            entity.ToTable("OrderStatus");
            entity.HasKey(o => o.Id);
        }
    }
}
