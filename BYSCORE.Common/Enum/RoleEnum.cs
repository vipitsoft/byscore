using System;
using System.ComponentModel;

namespace BYSCORE.Common
{
    public enum RoleEnum
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        SuperAdmin,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin,
    }
}
