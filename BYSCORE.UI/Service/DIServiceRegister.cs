using System;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.UI
{
    public class DIServiceRegister
    {
        public void DIRegister(IServiceCollection services)
        {
            services.AddTransient<ProductService>();
            services.AddTransient<LogService>();
            services.AddTransient<AppLogService>();
        }
    }
}
