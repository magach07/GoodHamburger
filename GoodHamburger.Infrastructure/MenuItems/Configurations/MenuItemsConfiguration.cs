using GoodHamburger.Domain.MenuItems.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.MenuItems.Configurations
{
    public class MenuItemsConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> entity)
        {
            entity.ToTable("MenuItem");
            entity.HasKey(mi => mi.Id);

            entity.HasOne(mi => mi.MenuItemType)
                .WithMany(mit => mit.MenuItems)
                .HasForeignKey(mi => mi.IdMenuItemType)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
