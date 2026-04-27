using GoodHamburger.Domain.MenuItemsTypes.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.MenuItemsTypes.Configurations
{
    public class MenuItemsTypesConfiguration : IEntityTypeConfiguration<MenuItemType>
    {
        public void Configure(EntityTypeBuilder<MenuItemType> entity)
        {
            entity.ToTable("MenuItemType");
            entity.HasKey(o => o.Id);
        }
    }
}