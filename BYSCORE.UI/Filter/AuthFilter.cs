using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Common.Consts;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BYSCORE.UI
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly ICacheService _cacheService;
        private readonly MenuService _menuService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;
        public AuthFilter(ICacheService cacheService, MenuService menuService, UserService userService, RoleService roleService)
        {
            _cacheService = cacheService;
            _menuService = menuService;
            _userService = userService;
            _roleService = roleService;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {

                    var claims = context.HttpContext.User.Claims;
                    // 获取用户id
                    var sid = claims.FirstOrDefault(t => t.Type == ClaimTypes.Sid).Value;
                    User user = _userService.Get(sid).Result;

                    string userInfoKey = $"{CacheKeyConst.SysKey}:{CacheKeyConst.UserKey}:{sid}:{ SysConsts.USERINFO}";
                    if (!_cacheService.Exists(userInfoKey))
                    {
                        _cacheService.Add(userInfoKey, user);
                    }

                    string menuKey = $"{CacheKeyConst.SysKey}:{CacheKeyConst.MenuKey}:{SysConsts.MENUALLLIST}";
                    if (!_cacheService.Exists(menuKey))
                    {
                        List<Menu> menus = _menuService.GetList().Result;
                        SetMenuList(menus); // 所有菜单
                        SetMenu(menus);
                    }

                    // 当前登录用户角色拥有权限
                    string roleKey = $"{CacheKeyConst.SysKey}:{CacheKeyConst.RoleKey}:{user.Role.Code}:{SysConsts.RoleMENUALL}";
                    if (!_cacheService.Exists(roleKey))
                    {
                        List<Menu> menus = _roleService.GetRoleMenus(user.Role.Code).Result;

                        _cacheService.Add(roleKey, menus);
                    }

                    string userMenuKey = $"{CacheKeyConst.SysKey}:{CacheKeyConst.UserKey}:{CacheKeyConst.MenuKey}:{sid}";
                    if (!_cacheService.Exists(userMenuKey))
                    {
                        List<Menu> userMenulist = _userService.GetUserMenus(sid).Result;

                        SetUserAllMenu(userMenulist, sid);
                        SetUserMenu(userMenulist, sid);
                    }

                }
                catch (Exception ex)
                {
                    nlog.Error(ex, "鉴权出错！");
                    context.Result = new ContentResult() { Content = "系统错误，请联系管理员 ！" };
                }

            }

        }

        #region 所有菜单
        /// <summary>
        ///  获取所有菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetMenuList(List<Menu> menus)
        {
            _cacheService.Add(SysConsts.MENUALLLIST, menus);
        }

        /// <summary>
        ///  获取菜单类型
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetMenu(List<Menu> menus)
        {
            _cacheService.Add(SysConsts.MENULIST, menus.Where(t => t.Type == (int)MenuType.Menu).ToList());
        }

        /// <summary>
        /// 获取按钮菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetButton(List<Menu> menus)
        {

        }

        /// <summary>
        /// 获取接口菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetData(List<Menu> menus)
        {

        }
        #endregion

        #region 用户菜单
        /// <summary>
        ///  获取用户所有菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        /// <param name="code">code.</param>
        private void SetUserAllMenu(List<Menu> menus, string code)
        {

            _cacheService.Add(code + "-" + SysConsts.USERMENUALL, menus);
        }

        /// <summary>
        ///  获取用户菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        /// <param name="code">code.</param>
        private void SetUserMenu(List<Menu> menus, string code)
        {

            menus = menus.Where(t => t.Type == (int)MenuType.Menu).OrderBy(o => o.Sort).ToList();

            _cacheService.Add(code + "-" + SysConsts.USERMENULIST, menus);
        }

        /// <summary>
        /// 获取用户按钮菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetUserButton(List<Menu> menus)
        {

        }

        /// <summary>
        /// 获取用户接口菜单
        /// </summary>
        /// <param name="menus">Menus.</param>
        private void SetUserData(List<Menu> menus)
        {

        }
        #endregion

    }
}