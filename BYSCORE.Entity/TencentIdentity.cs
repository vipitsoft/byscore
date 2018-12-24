using System;
using System.Security.Principal;

namespace BYSCORE.Entity
{
    public class TencentIdentity: IIdentity
    {
        public TencentIdentity(string name, int staffId, string[] roles, string[] rights)
        {
            Name = name;
            StaffId = staffId;
            Roles = roles;
            Rights = rights;
        }

        public string AuthenticationType
        {
            get { return "TencentIdentity"; }
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 登录账号对应的员工ID
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// 登录账号在系统所拥有的角色
        /// </summary>
        public string[] Roles { get; set; }
        public string[] Rights { get; set; }

        public string ChineseName { get; set; }

        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(ChineseName))
                {
                    return string.Format("{0}({1})", Name, ChineseName);
                }
                return Name;
            }
        }
    }
}
