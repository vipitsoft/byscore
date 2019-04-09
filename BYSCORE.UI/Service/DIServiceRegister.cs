using System;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.UI
{
    public static class DIServiceRegister
    {
        public static void DIRegister(IServiceCollection services)
        {
            services.AddTransient<ProductService>();
            services.AddTransient<LogService>();
            services.AddTransient<AppLogService>();
        }
    }
}
