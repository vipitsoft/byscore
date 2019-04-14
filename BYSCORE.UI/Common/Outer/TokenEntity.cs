using System;
namespace BYSCORE.UI.Common.Outer
{
    /// <summary>
    /// Token实体
    /// </summary>
    public class TokenEntity
    {
        /// <summary>
        /// 登录状态
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// token字符串
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时差
        /// </summary>
        /// <value>The expires in.</value>
        public int Expires { get; set; }

        /// <summary>
        /// 用户code
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserCode { get; set; }
    }
}
