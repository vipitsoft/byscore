using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.ToTable("role_menu");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Code);
            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.IsDelete).HasColumnType("bit");
            builder.Property(t => t.MenuId);
            builder.Property(t => t.RoleId);
        }
    }
}
