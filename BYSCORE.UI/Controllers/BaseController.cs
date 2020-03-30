using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using BYSCORE.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.UI.Controllers
{
    public class BaseController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            ICacheService _cacheService = (ICacheService)context.HttpContext.RequestServices.GetService(typeof(ICacheService));
            string userCode = claimIdentity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid).Value;
            // 用户信息
            string userKey = userCode + "-" + SysConsts.USERINFO;
            User user = null;
            if (!_cacheService.Exists(userKey))
            {
                UserService userService = (UserService)context.HttpContext.RequestServices.GetService(typeof(UserService));
                user = userService.Get(userCode).Result;
                _cacheService.Add(userCode, user);
            }
            else
            {
                user = _cacheService.Get<User>(userKey);
            }

            string controller = context.RouteData.Values["Controller"].ToString();
            string action = context.RouteData.Values["Action"].ToString();
            string url = ("/" + controller + "/" + action).ToLower();
            List<Menu> usermenuAllList = _cacheService.Get<List<Menu>>(user.Code + "-" + SysConsts.USERMENUALL);
            List<Menu> roleMenuList = _cacheService.Get<List<Menu>>(user.Role.Code + "-" + SysConsts.RoleMENUALL);
            if (!usermenuAllList.Any(t => t.Url.Contains(url)) && !roleMenuList.Any(t => t.Url.Contains(url)))
            {
                context.Result = new ContentResult() { Content = string.Format("抱歉，您没有{0}的权限！", url) };
            }


            ViewBag.UserInfo = user;

            base.OnActionExecuting(context);
        }


    }
}

