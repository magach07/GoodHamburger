using GoodHamburger.Domain.OrderItems.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.OrderItems.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.ToTable("OrderItem");
            entity.HasKey(oi => oi.Id);

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.IdMenuItem)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}