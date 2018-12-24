using System;
using System.Linq;
using System.Security.Principal;

namespace BYSCORE.Entity
{
    public class TencentPrincipal: IPrincipal
    {
        public TencentPrincipal(TencentIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        private TencentIdentity tencentIdentity
        {
            get
            {
                return Identity as TencentIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            try
            {
                if (!string.IsNullOrEmpty(role) && tencentIdentity.Roles != null)
                {
                    foreach (var item in role.Split(','))
                    {
                        if (tencentIdentity.Roles.Contains(item))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
