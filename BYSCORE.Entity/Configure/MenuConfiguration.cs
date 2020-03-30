using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {

        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("menu");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.IsDelete).HasColumnType("tinyint(1)");
            builder.Property(t => t.Level);
            builder.Property(t => t.Code);
            builder.Property(t => t.Icon);
            builder.Property(t => t.CName);
            builder.Property(t => t.EName);
            builder.Property(t => t.ParentId);
            builder.Property(t => t.Remarks);
            builder.Property(t => t.Type);
            builder.Property(t => t.Url);
            builder.Property(t => t.Sort);
        }
    }
}
