using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BYSCORE.Entity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Code);
            builder.Property(t => t.CreatedTime);
            builder.Property(t => t.Email);
            builder.Property(t => t.HeadImg);
            builder.Property(t => t.IsDelete).HasColumnType("tinyint(1)");
            builder.Property(t => t.IsFreeze).HasColumnType("tinyint(1)");
            builder.Property(t => t.LastLoginTime);
            builder.Property(t => t.Phone);
            builder.Property(t => t.UserName);
            builder.Property(t => t.Name);
            builder.Property(t => t.UserNumber);
            builder.Property(t => t.PassWord);
            builder.Property(t => t.RoleId);

            // 排除字段
            builder.Ignore(t => t.MenuIds);
            builder.Ignore(t => t.RoleName);
            builder.Ignore(t => t.DepartmentName);
            builder.Ignore(t => t.AreaName);
            builder.Ignore(t => t.UpType);

            builder.HasOne(t => t.Role).WithMany().HasForeignKey(t => t.RoleId);

            builder.HasMany(t => t.UserMenus).WithOne().HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.Department).WithMany().HasForeignKey(t => t.DepartmentId);

            builder.HasOne(t => t.Area).WithMany().HasForeignKey(t => t.AreaId);
        }
    }
}
