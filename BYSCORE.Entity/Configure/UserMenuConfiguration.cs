using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class UserMenuConfiguration : IEntityTypeConfiguration<UserMenu>
    {

        public void Configure(EntityTypeBuilder<UserMenu> builder)
        {
            builder.ToTable("user_menu");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id);
            builder.Property(t => t.Code);
            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.IsDelete).HasColumnType("bit");
            builder.Property(t => t.MenuId);
            builder.Property(t => t.UserId);
        }
    }
}
