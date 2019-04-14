using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BYSCORE.UI
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly ICacheService _cacheService;
        private readonly MenuService _menuService;
        private readonly UserService _userService;
        public AuthFilter(ICacheService cacheService, MenuService menuService, UserService userService)
        {
            _cacheService = cacheService;
            _menuService = menuService;
            _userService = userService;
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

                    if (!_cacheService.Exists(sid + "-" + SysConsts.USERINFO))
                    {
                        UserService userService = (UserService)context.HttpContext.RequestServices.GetService(typeof(UserService));
                        User user = userService.Get(sid).Result;

                        _cacheService.Add(sid + "-" + SysConsts.USERINFO, user);
                    }


                    if (!_cacheService.Exists(SysConsts.MENUALLLIST))
                    {
                        List<Menu> menus = _menuService.GetList().Result;
                        SetMenuList(menus); // 所有菜单
                        SetMenu(menus);
                    }

                    if (!_cacheService.Exists(sid + "-" + SysConsts.USERMENUALL))
                    {
                        List<Menu> userMenulist = _userService.GetUserMenus(sid).Result;
                        if (userMenulist == null && userMenulist.Count == 0)
                        {
                            context.Result = new ContentResult() { Content = "抱歉，您没有权限！" };
                        }
                        string controller = context.RouteData.Values["Controller"].ToString();
                        string action = context.RouteData.Values["Action"].ToString();
                        string url = ("/" + controller + "/" + action).ToLower();
                        if (!userMenulist.Any(t => t.Url.Contains(url)))
                        {
                            context.Result = new ContentResult() { Content = "抱歉，您没有权限！" };
                        }


                        SetUserAllMenu(userMenulist, sid);
                        SetUserMenu(userMenulist, sid);
                    }

                }
                catch (Exception ex)
                {
                    LogService logService = (LogService)context.HttpContext.RequestServices.GetService(typeof(LogService));
                    logService.Error("系统错误！", ex);
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