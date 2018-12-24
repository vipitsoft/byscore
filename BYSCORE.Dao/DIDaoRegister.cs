using System;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.Dao
{
    public class DIDaoRegister
    {
        public void DIRegister(IServiceCollection services)
        {
            services.AddTransient<ProductDao>();
            services.AddTransient<ApplicationLogDao>();
        }
    }
}
