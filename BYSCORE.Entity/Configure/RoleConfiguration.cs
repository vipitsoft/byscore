using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id);
            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.IsDelete).HasColumnType("tinyint(1)");
            builder.Property(t => t.Code);
            builder.Property(t => t.Description);
            builder.Property(t => t.CName);
            builder.Property(t => t.EName);

            builder.Ignore(t => t.MenuIds);

        }
    }
}
