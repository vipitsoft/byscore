using System;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.UI
{
    public static class DIServiceRegister
    {
        public static void DIRegister(IServiceCollection services)
        {
            services.AddTransient<LogService>();
            services.AddTransient<AppLogService>();
            services.AddTransient<ConfigInfoService>();
            services.AddTransient<MenuService>();
            services.AddTransient<RoleService>();
            services.AddTransient<RoleMenuService>();
            services.AddTransient<UserMenuService>();
            services.AddTransient<UserService>();
        }
    }
}
