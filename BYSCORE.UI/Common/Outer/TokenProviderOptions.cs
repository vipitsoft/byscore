using System;
using Microsoft.IdentityModel.Tokens;

namespace BYSCORE.UI.Common.Outer
{
    public class TokenProviderOptions
    {
        /// <summary>
        /// 发行人
        /// </summary>
        /// <value>The issuer.</value>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        /// <value>The audience.</value>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间间隔
        /// </summary>
        /// <value>The expiration.</value>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(720);

        /// <summary>
        /// 签名证书
        /// </summary>
        /// <value>The signing credentials.</value>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
