using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name);
            builder.Property(t => t.Description);
            builder.Property(t => t.Category);
            builder.Property(t => t.Price);
            builder.Property(t => t.Discout);
        }
    }
}
