using System;
using Microsoft.AspNetCore.Authorization;

namespace BYSCORE.UI.Common.Outer
{
    public class WebApiAuthroizeAttribute : AuthorizeAttribute
    {
        public const string AuthenticationScheme = "AppletsWebApiAuthenticationScheme";

        public WebApiAuthroizeAttribute()
        {
            this.AuthenticationSchemes = AuthenticationScheme;
        }
    }
}
