using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class ConfigInfoConfiguration : IEntityTypeConfiguration<ConfigInfo>
    {

        public void Configure(EntityTypeBuilder<ConfigInfo> builder)
        {
            builder.ToTable("config_info");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id);
            builder.Property(t => t.Code);
            builder.Property(t => t.Name);
            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.IsDelete).HasColumnType("bit");
            builder.Property(t => t.ParentId);
            builder.Property(t => t.Remarks);

            builder.Ignore(t => t.ParentName);
        }
    }
}
