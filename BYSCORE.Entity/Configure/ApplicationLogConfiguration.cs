using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class ApplicationLogConfiguration : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {

            builder.ToTable("applicationlog");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Application);
            builder.Property(t => t.Level);
            builder.Property(t => t.Logged);
            builder.Property(t => t.Callsite);
            builder.Property(t => t.Message);
            builder.Property(t => t.Logger);
            builder.Property(t => t.Exception);
            builder.Property(t => t.Text).HasColumnType("text");
        }
    }
}
