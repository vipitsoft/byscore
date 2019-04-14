using System;

namespace BYSCORE.UI
{
    public class ApiUrls
    {

        internal static class Log
        {
            private const string prefix = "/api/log/";

            public const string Debug = prefix + "debug";

            public const string Info = prefix + "info";

            public const string Error = prefix + "error";
        }

        internal static class AppLog
        {
            private const string prefix = "/api/applog/";

            public const string GetAppLogs = prefix + "getapplogs";

            public const string GetAppLog = prefix + "getapplog";
        }

        internal static class User
        {
            private const string prefix = "/api/user/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";

            public const string UserLogin = prefix + "userlogin";

            public const string GetUserMenus = prefix + "getUserMenus";

            public const string FreezeUser = prefix + "freezeUser";

            public const string UpUserPwd = prefix + "upUserPwd";

            public const string GetUserNameList = prefix + "getUserNameList";

            public const string UpUser = prefix + "upUser";

        }

        internal static class UserMenu
        {
            private const string prefix = "/api/usermenu/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";

            public const string GetListByUserId = prefix + "getListByUserId";
        }

        internal static class UserRole
        {
            private const string prefix = "/api/userrole/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";
        }

        internal static class Menu
        {
            private const string prefix = "/api/menu/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";
        }

        internal static class Role
        {
            private const string prefix = "/api/role/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";

            public const string GetRoleMenus = prefix + "getRoleMenus";
        }

        internal static class RoleMenu
        {
            private const string prefix = "/api/rolemenu/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";

            public const string GetListByRoleId = prefix + "getListByRoleId";
        }

        internal static class ConfigInfo
        {
            private const string prefix = "/api/configinfo/";

            public const string Add = prefix + "add";

            public const string GetListByPage = prefix + "getListByPage";

            public const string Getlist = prefix + "getlist";

            public const string GetListByType = prefix + "getlistbytype";

            public const string Get = prefix + "get";

            public const string Delete = prefix + "delete";

            public const string Update = prefix + "update";

            public const string GetByname = prefix + "getbyname";

            public const string PullSalary = prefix + "pullsalary";
        }
    }
}
