using System;
using System.Collections.Generic;
using BYSCORE.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BYSCORE.Dao
{
    public class DIDaoRegister
    {
        public void DIRegister(IServiceCollection services)
        {
            List<Type> blllist = Tools.GetClass(typeof(DIDaoRegister).Namespace);
            foreach (var item in blllist)
            {
                if (item.FullName.Contains("Dao"))
                {
                    services.AddTransient(item);
                }
            }
        }
    }
}
