using System;
using System.Collections;
using System.Collections.Generic;

namespace BYSCORE.Entity
{
    public class PublicView
    {
        public int TotalCount { get; set; }

        public IEnumerable<ApplicationLog> ApplicationLogList { get; set; }

        public IEnumerable<ConfigInfo> ConfigInfoList { get; set; }

        public IEnumerable<Menu> MenuList { get; set; }

        public IEnumerable<Role> RoleList { get; set; }

        public IEnumerable<RoleMenu> RoleMenuList { get; set; }

        public IEnumerable<User> UserList { get; set; }

        public IEnumerable<UserMenu> UserMenuList { get; set; }
    }
}
