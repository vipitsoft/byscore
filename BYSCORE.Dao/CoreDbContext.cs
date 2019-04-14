using System;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public CoreDbContext()
        {
        }

        public DbSet<ApplicationLog> ApplicationLog { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserMenu> UserMenu { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<ConfigInfo> ConfigInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationLogConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserMenuConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigInfoConfiguration());
        }
    }


}

